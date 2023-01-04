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
    public class AdminTicketsController : Controller
    {
        private readonly bookticketsContext _context;

        public AdminTicketsController(bookticketsContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTickets
        public async Task<IActionResult> Index()
        {
            var bookticketsContext = _context.Tickets.Include(t => t.IdOderNavigation).Include(t => t.IdShowtimeNavigation);
            return View(await bookticketsContext.ToListAsync());
        }

        // GET: Admin/AdminTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.IdOderNavigation)
                .Include(t => t.IdShowtimeNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Admin/AdminTickets/Create
        public IActionResult Create()
        {
            ViewData["IdOder"] = new SelectList(_context.Oders, "IdOder", "IdOder");
            ViewData["IdShowtime"] = new SelectList(_context.Showtimes, "IdShowtime", "IdShowtime");
            return View();
        }

        // POST: Admin/AdminTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTicket,IdOder,IdShowtime,Dongia")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOder"] = new SelectList(_context.Oders, "IdOder", "IdOder", ticket.IdOder);
            ViewData["IdShowtime"] = new SelectList(_context.Showtimes, "IdShowtime", "IdShowtime", ticket.IdShowtime);
            return View(ticket);
        }

        // GET: Admin/AdminTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["IdOder"] = new SelectList(_context.Oders, "IdOder", "IdOder", ticket.IdOder);
            ViewData["IdShowtime"] = new SelectList(_context.Showtimes, "IdShowtime", "IdShowtime", ticket.IdShowtime);
            return View(ticket);
        }

        // POST: Admin/AdminTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTicket,IdOder,IdShowtime,Dongia")] Ticket ticket)
        {
            if (id != ticket.IdTicket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.IdTicket))
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
            ViewData["IdOder"] = new SelectList(_context.Oders, "IdOder", "IdOder", ticket.IdOder);
            ViewData["IdShowtime"] = new SelectList(_context.Showtimes, "IdShowtime", "IdShowtime", ticket.IdShowtime);
            return View(ticket);
        }

        // GET: Admin/AdminTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.IdOderNavigation)
                .Include(t => t.IdShowtimeNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Admin/AdminTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.IdTicket == id);
        }
    }
}
