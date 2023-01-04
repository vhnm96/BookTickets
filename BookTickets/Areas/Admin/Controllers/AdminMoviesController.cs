using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookTickets.Models;
using BookTickets.Helpper;
using System.IO;

namespace BookTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMoviesController : Controller
    {
        private readonly bookticketsContext _context;

        public AdminMoviesController(bookticketsContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminMovies
        public async Task<IActionResult> Index()
        {
            var bookticketsContext = _context.Movies.Include(m => m.IdCategoryNavigation);
            return View(await bookticketsContext.ToListAsync());
        }

        // GET: Admin/AdminMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdMovie == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Admin/AdminMovies/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "NameCategory");
            return View();
        }

        // POST: Admin/AdminMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovie,NameMovie,Mota,Anhbia,IdCategory,Alias,DateModified,DateCreated")] Movie movie, Microsoft.AspNetCore.Http.IFormFile Anhbia)
        {

            if (ModelState.IsValid)
            {
                movie.NameMovie = Utilities.ToTitleCase(movie.NameMovie);
                if (Anhbia != null)
                {
                    string extension = Path.GetExtension(Anhbia.FileName);
                    string image = Utilities.SEOUrl(movie.NameMovie) + extension;
                    movie.Anhbia = await Utilities.UploadFile(Anhbia, @"movie", image.ToLower());
                }
                if (string.IsNullOrEmpty(movie.Anhbia)) movie.Anhbia = "default.jpg";
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "NameCategory", movie.IdCategory);
            return View(movie);
        }

        // GET: Admin/AdminMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "NameCategory", movie.IdCategory);
            return View(movie);
        }

        // POST: Admin/AdminMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMovie,NameMovie,Mota,Anhbia,IdCategory")] Movie movie)
        {
            if (id != movie.IdMovie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.IdMovie))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "NameCategory", movie.IdCategory);
            return View(movie);
        }

        // GET: Admin/AdminMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdMovie == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Admin/AdminMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.IdMovie == id);
        }
    }
}
