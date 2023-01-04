using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTickets.Controllers
{
    public class OderController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
