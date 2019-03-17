using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_I_M32COM.Data;
using Group_I_M32COM.DbTableModel;
using Microsoft.AspNetCore.Authorization;
using Group_I_M32COM.Extensions.Alerts;

namespace Group_I_M32COM.Controllers
{
    /* We use the Authroize Data Annotation to assign the role based authorization in EventsController access level
       The authorize data annotation will check if the user is logged and retrieves the user role
       If the user is not logged in it will redirect the user to the login page */
    [Authorize(Roles = "Admin, TeamLeader")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            // To read related data from the event_type table and boat_type table to display event details
            var _events = _context.Events
                .Include(e => e.Event_Types)
                .Include(b => b.Boat_Types);
            return View(await _events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To retrieve the boats available in the Boat_Types table and display the available records 
                       in the dropdown list*/
                    var boat_type = _context.Boat_Types
                        .Select(a => new SelectListItem { Text = a.Boat_class_type, Value = a.Id.ToString() })
                        .ToList();
                    boat_type.Insert(0, new SelectListItem { Text = "Select Boat Type", Value = string.Empty });
                    ViewBag.Boat_type = boat_type;

                    /* To retrieve the boats available in the Event_Types table and display the available records 
                       in the dropdown list*/
                    var event_type = _context.Event_Types
                        .Select(e => new SelectListItem { Text = e.Event_type_name, Value = e.Id.ToString() })
                        .ToList();
                    event_type.Insert(0, new SelectListItem { Text = "Select Event Type", Value = string.Empty });
                    ViewBag.Event_type = event_type;

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
                
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Event_name,Event_description,Event_Start_date,Event_End_date,Created_At,Updated_At")] Event @event, string Event_Types, string Boat_Types)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To return the selected event_type name and boat_type class from the database if it exists*/
                    var get_event_type = _context.Event_Types.SingleOrDefault(x => x.Id == Convert.ToInt32(Event_Types));
                    @event.Event_Types = get_event_type;

                    var get_boat_type = _context.Boat_Types.SingleOrDefault(x => x.Id == Convert.ToInt32(Boat_Types));
                    @event.Boat_Types = get_boat_type;

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
                @event.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Inserted Event Details");
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var @event = await _context.Events.FindAsync(id);
            var @event = await _context.Events
                .Include(et => et.Event_Types)
                .Include(bt => bt.Boat_Types)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To retrieve the boats available in the Boat_Types table and display the available records 
                       in the dropdown list*/
                    var boat_type = _context.Boat_Types
                        .Select(a => new SelectListItem
                        {
                            Text = a.Boat_class_type,
                            Value = a.Id.ToString(),
                            Selected = a.Id == @event.Boat_Types.Id ? true : false
                        })
                        .ToList();
                    boat_type.Insert(0, new SelectListItem { Text = "Select Boat Type", Value = string.Empty });
                    ViewBag.Boat_type = boat_type;

                    /* To retrieve the boats available in the Event_Types table and display the available records 
                       in the dropdown list*/
                    var event_type = _context.Event_Types
                        .Select(e => new SelectListItem
                        {
                            Text = e.Event_type_name,
                            Value = e.Id.ToString(),
                            Selected = e.Id == @event.Event_Types.Id ? true : false
                        })
                        .ToList();
                    event_type.Insert(0, new SelectListItem { Text = "Select Event Type", Value = string.Empty });
                    ViewBag.Event_type = event_type;

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
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Event_name,Event_description,Event_Start_date,Event_End_date,Created_At,Updated_At")] Event @event, string Event_Types, string Boat_Types)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            /* To return the selected event_type name and boat_type class from the database if it exists*/
            var get_event_type = _context.Event_Types.SingleOrDefault(x => x.Id == Convert.ToInt32(Event_Types));
            @event.Event_Types = get_event_type;

            var get_boat_type = _context.Boat_Types.SingleOrDefault(x => x.Id == Convert.ToInt32(Boat_Types));
            @event.Boat_Types = get_boat_type;

            if (ModelState.IsValid)
            {
                try
                {
                    // To set the system time for record update
                    @event.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(@event);
                    _context.Entry(@event).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Updated Event Details");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Deleted Event Details");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
