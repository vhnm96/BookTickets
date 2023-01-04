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
    public class AdminOdersController : Controller
    {
        private readonly bookticketsContext _context;

        public AdminOdersController(bookticketsContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminOders
        public async Task<IActionResult> Index()
        {
            var bookticketsContext = _context.Oders.Include(o => o.MaKhNavigation);
            return View(await bookticketsContext.ToListAsync());
        }

        // GET: Admin/AdminOders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oder = await _context.Oders
                .Include(o => o.MaKhNavigation)
                .FirstOrDefaultAsync(m => m.IdOder == id);
            if (oder == null)
            {
                return NotFound();
            }

            return View(oder);
        }

        // GET: Admin/AdminOders/Create
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "HoTen");
            return View();
        }

        // POST: Admin/AdminOders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOder,Dathanhtoan,Ngaydat,MaKh")] Oder oder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "HoTen", oder.MaKh);
            return View(oder);
        }

        // GET: Admin/AdminOders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oder = await _context.Oders.FindAsync(id);
            if (oder == null)
            {
                return NotFound();
            }
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "HoTen", oder.MaKh);
            return View(oder);
        }

        // POST: Admin/AdminOders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOder,Dathanhtoan,Ngaydat,MaKh")] Oder oder)
        {
            if (id != oder.IdOder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OderExists(oder.IdOder))
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
            ViewData["MaKh"] = new SelectList(_context.Khachhangs, "MaKh", "HoTen", oder.MaKh);
            return View(oder);
        }

        // GET: Admin/AdminOders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oder = await _context.Oders
                .Include(o => o.MaKhNavigation)
                .FirstOrDefaultAsync(m => m.IdOder == id);
            if (oder == null)
            {
                return NotFound();
            }

            return View(oder);
        }

        // POST: Admin/AdminOders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oder = await _context.Oders.FindAsync(id);
            _context.Oders.Remove(oder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OderExists(int id)
        {
            return _context.Oders.Any(e => e.IdOder == id);
        }
    }
}
