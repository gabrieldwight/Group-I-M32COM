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

        public async Task<IActionResult> Index()
        {
            // To load the list of boat details to the homepage
            var _boats = _context.Boats
                .Include(boat_media => boat_media.Boat_Medias)
              .Include(boat_category => boat_category.Boat_Types)
              .Include(sub_boat_category => sub_boat_category.Sub_Boat_Types);
            return View(await _boats.ToListAsync());
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

        public async Task <IActionResult> Crew()
        {
            // To get the boat crew details and member details  and event participations
            var boat_crew = _context.Boat_Crews
                .Include(bm => bm.Members)
                .Include(ep => ep.Event_Participations);
            return View(await boat_crew.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
