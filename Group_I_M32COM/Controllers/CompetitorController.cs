using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_I_M32COM.Data;
using Group_I_M32COM.DbTableModel;
using Group_I_M32COM.Extensions.Alerts;
using Group_I_M32COM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Group_I_M32COM.Controllers
{
    /* We use the Authroize Data Annotation to assign the role based authorization in EventsController access level
       The authorize data annotation will check if the user is logged and retrieves the user role
       If the user is not logged in it will redirect the user to the login page */
    [Authorize(Roles = "TeamLeader")]
    public class CompetitorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Competitor team list
        public async Task<IActionResult> Index()
        {
            var team_data = await _context.Boat_Crews.FirstOrDefaultAsync(m => m.Id == 1);
            ViewBag.Slots = team_data.Boat_crew_allocation;

            return View("~/Views/Competitor/TeamMembers.cshtml", await _context.Members.ToListAsync());
        }

        // GET: Competitor participated in event
        public async Task<IActionResult> EventParticipated()
        {
            var event_data = _context.Event_Participations
                .Include(e => e.Event);
            return View("~/Views/Competitor/EventsParticipated.cshtml", await event_data.ToListAsync());
        }

        // GET: Competitor/Details/5
        public IActionResult Details(string id)
        {
            string user_id = id.ToString();
            var applicationUser = from r in _context.Roles
                                  join ru in _context.UserRoles on r.Id equals ru.RoleId
                                  join u in _context.Users on ru.UserId equals u.Id
                                  where u.Id == user_id
                                  select new User_RoleModel
                                  {
                                    Role_Name = r.Name,
                                    First_Name = u.FirstName,
                                    Last_Name = u.LastName,
                                    Address = u.Address,
                                    Email= u.Email,                                       
                                  };
                                  
            return View(applicationUser.Single());
        }

        // To display event registration form
        // GET: Competitor/RegisterEvent_Create
        public IActionResult RegisterEvent_Create()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // To load the available events from the database
                    var events_available = _context.Events
                        .Select(e => new SelectListItem
                        {
                            Text = e.Event_name,
                            Value = e.Id.ToString(),
                        })
                        .OrderBy(o => o.Text).ToList();
                    events_available.Insert(0, new SelectListItem { Text = "Please select event to participate in", Value = string.Empty });
                    ViewBag.Events = events_available;

                    // Commit the transaction in the above number operations of the database context
                    dbContextTransaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    // In case of errors committed in the transaction. Changes will be rollback to the previous state
                    dbContextTransaction.Rollback();
                }
            }
            return View("~/Views/Competitor/RegisterEvent.cshtml");
        }

        // To register a boat team to participate in an event
        // POST: Competitor/RegisterEvent_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterEvent_Create([Bind("Id,position,points_awarded,Created_At,Updated_At")] Event_participation event_Participation, string Event)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To return the selected event name from the database if it exists*/
                    var get_event = _context.Events.SingleOrDefault(x => x.Id == Convert.ToInt32(Event));
                    event_Participation.Event = get_event;

                    var team_data = await _context.Boat_crew_leader
                        .Include(bc => bc.boat_Crew)
                        .FirstOrDefaultAsync(m => m.User_Id == TempData["User_Id"].ToString());

                    event_Participation.boat_Crew = team_data.boat_Crew;

                    // Commit the transaction in the above number operations of the database context
                    dbContextTransaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    // In case of errors committed in the transaction. Changes will be rollback to the previous state
                    dbContextTransaction.Rollback();
                }
            }

            if (ModelState.IsValid)
            {
                // To pass the creation date on system time
                event_Participation.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(event_Participation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EventParticipated)).WithSuccess("Success", "Successfully Registered to participate in the event");
            }
            return View(event_Participation);
        }

        // GET: Competitor/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Competitor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Competitor/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Competitor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}