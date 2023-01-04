using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTickets.Models;

    public class ProductController : Controller
    {
    private readonly bookticketsContext _context;
    public ProductController(bookticketsContext context)
    {
        _context = context;
    }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
        return View();
        }
}

