using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_I_M32COM.Data;
using Group_I_M32COM.DbTableModel;
using Group_I_M32COM.Extensions.Alerts;

namespace Group_I_M32COM.Controllers
{
    public class Boat_typeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Boat_typeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boat_type
        public async Task<IActionResult> Index()
        {
            return View(await _context.Boat_Types.ToListAsync());
        }

        // GET: Boat_type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat_type = await _context.Boat_Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boat_type == null)
            {
                return NotFound();
            }

            return View(boat_type);
        }

        // GET: Boat_type/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boat_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Boat_class_type,Created_At,Updated_At")] Boat_type boat_type, List<Sub_boat_type> sub_Boat_Types)
        {
            boat_type.Sub_Boat_Types = new List<Sub_boat_type>();
            if (ModelState.IsValid)
            {
                foreach(var sub_category in sub_Boat_Types)
                {
                    boat_type.Sub_Boat_Types.Add(new Sub_boat_type
                    {
                        Sub_boat_class_type = sub_category.Sub_boat_class_type,
                        Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                    });
                }
                // To pass the creation date on system time
                boat_type.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(boat_type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Inserted Boat Category Details");
            }
            return View(boat_type);
        }

        // GET: Boat_type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat_type = await _context.Boat_Types.FindAsync(id);
            if (boat_type == null)
            {
                return NotFound();
            }
            return View(boat_type);
        }

        // POST: Boat_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Boat_class_type,Created_At,Updated_At")] Boat_type boat_type)
        {
            if (id != boat_type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // To set the system time for record update
                    boat_type.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(boat_type);
                    _context.Entry(boat_type).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Boat_typeExists(boat_type.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Updated Boat Category Details");
            }
            return View(boat_type);
        }

        // GET: Boat_type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat_type = await _context.Boat_Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boat_type == null)
            {
                return NotFound();
            }

            return View(boat_type);
        }

        // POST: Boat_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Delete the boat type record from the boat type table
                    var boat_type = await _context.Boat_Types.FindAsync(id);
                    _context.Boat_Types.Remove(boat_type);

                    // To get the related boat ID Foreign key from the boat media table
                    var boat_sub_type = _context.Sub_Boat_Types.Where(b => b.Boat_Types.Id == boat_type.Id);
                    foreach (var boat_sub_category in boat_sub_type)
                    {
                        _context.Sub_Boat_Types.Remove(boat_sub_category);
                    }
                    await _context.SaveChangesAsync();
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
            return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Deleted Boat Category Details");
        }

        private bool Boat_typeExists(int id)
        {
            return _context.Boat_Types.Any(e => e.Id == id);
        }
    }
}
