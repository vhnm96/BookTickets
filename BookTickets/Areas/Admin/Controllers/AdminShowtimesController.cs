using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookTickets.Models;

namespace BookTickets.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminShowtimesController : Controller
    {
        private readonly bookticketsContext _context;

        public AdminShowtimesController(bookticketsContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminShowtimes
        public async Task<IActionResult> Index()
        {
            var bookticketsContext = _context.Showtimes.Include(s => s.IdMovieNavigation).Include(s => s.IdRoomNavigation);
            return View(await bookticketsContext.ToListAsync());
        }

        // GET: Admin/AdminShowtimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes
                .Include(s => s.IdMovieNavigation)
                .Include(s => s.IdRoomNavigation)
                .FirstOrDefaultAsync(m => m.IdShowtime == id);
            if (showtime == null)
            {
                return NotFound();
            }

            return View(showtime);
        }

        // GET: Admin/AdminShowtimes/Create
        public IActionResult Create()
        {
            ViewData["IdMovie"] = new SelectList(_context.Movies, "IdMovie", "NameMovie");
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom");
            return View();
        }

        // POST: Admin/AdminShowtimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdShowtime,IdMovie,IdRoom,Thoigianchieu,Price")] Showtime showtime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showtime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMovie"] = new SelectList(_context.Movies, "IdMovie", "NameMovie", showtime.IdMovie);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", showtime.IdRoom);
            return View(showtime);
        }

        // GET: Admin/AdminShowtimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes.FindAsync(id);
            if (showtime == null)
            {
                return NotFound();
            }
            ViewData["IdMovie"] = new SelectList(_context.Movies, "IdMovie", "NameMovie", showtime.IdMovie);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", showtime.IdRoom);
            return View(showtime);
        }

        // POST: Admin/AdminShowtimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdShowtime,IdMovie,IdRoom,Thoigianchieu,Price")] Showtime showtime)
        {
            if (id != showtime.IdShowtime)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showtime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowtimeExists(showtime.IdShowtime))
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
            ViewData["IdMovie"] = new SelectList(_context.Movies, "IdMovie", "NameMovie", showtime.IdMovie);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", showtime.IdRoom);
            return View(showtime);
        }

        // GET: Admin/AdminShowtimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes
                .Include(s => s.IdMovieNavigation)
                .Include(s => s.IdRoomNavigation)
                .FirstOrDefaultAsync(m => m.IdShowtime == id);
            if (showtime == null)
            {
                return NotFound();
            }

            return View(showtime);
        }

        // POST: Admin/AdminShowtimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showtime = await _context.Showtimes.FindAsync(id);
            _context.Showtimes.Remove(showtime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowtimeExists(int id)
        {
            return _context.Showtimes.Any(e => e.IdShowtime == id);
        }
    }
}
