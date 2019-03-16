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
    /* We use the Authroize Data Annotation to assign the role based authorization in controller access level
       The authorize data annotation will check if the user is logged and retrieves the user role
       If the user is not logged in it will redirect the user to the login page */
    [Authorize(Roles = "Admin")]
    public class Event_typeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Event_typeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event_type: loads the page view of details of event type
        public async Task<IActionResult> Index()
        {
            return View(await _context.Event_Types.ToListAsync());
        }

        // GET: Event_type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var event_type = await _context.Event_Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (event_type == null)
            {
                return NotFound();
            }

            return View(event_type);
        }

        // GET: Event_type/Create : loads page displaying form for creating new event type record
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Event_type_name,Created_At,Updated_At")] Event_type event_type)
        {
            if (ModelState.IsValid)
            {
                event_type.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(event_type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Inserted Event type Details");
            }
            return View(event_type);
        }

        // GET: Event_type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var event_type = await _context.Event_Types.FindAsync(id);
            if (event_type == null)
            {
                return NotFound();
            }
            return View(event_type);
        }

        // POST: Event_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Event_type_name,Created_At,Updated_At")] Event_type event_type)
        {
            if (id != event_type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    event_type.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(event_type);
                    _context.Entry(event_type).Property(x => x.Created_At).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Event_typeExists(event_type.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully updated event type Details");
            }
            return View(event_type);
        }

        // GET: Event_type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var event_type = await _context.Event_Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (event_type == null)
            {
                return NotFound();
            }

            return View(event_type);
        }

        // POST: Event_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var event_type = await _context.Event_Types.FindAsync(id);
            _context.Event_Types.Remove(event_type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully deleted event type Details");
        }

        private bool Event_typeExists(int id)
        {
            return _context.Event_Types.Any(e => e.Id == id);
        }
    }
}
