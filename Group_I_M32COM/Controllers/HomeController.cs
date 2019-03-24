using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Group_I_M32COM.Models;
using Group_I_M32COM.Data;
using Microsoft.EntityFrameworkCore;

namespace Group_I_M32COM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your Boat racing application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task <IActionResult> Events()
        {
            var event_data = _context.Events
                .Include(et => et.Event_Types)
                .Where(e => DateTime.Parse(e.Event_Start_date.Value.Date.ToString("yyyy-MM-dd")) >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
            return View(await event_data.ToListAsync());
        }

        public IActionResult Calendar()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
