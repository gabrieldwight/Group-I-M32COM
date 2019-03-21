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
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index(string id)
        {
            //var team_data = await _context.Boat_Crews.FirstOrDefaultAsync(m => m.Id == id);
            var team_data = await _context.Boat_crew_leader
                .Include(bc => bc.boat_Crew)
                .FirstOrDefaultAsync(m => m.User_Id == id);
            ViewBag.Slots = team_data.boat_Crew.Boat_crew_allocation;

            TempData["User_Id"] = team_data.User_Id;

            // To check if the user has a team role

            if(team_data.User_Id != null)
            {
                return View(await _context.Members.ToListAsync());
            }

            else
            {
                return NotFound();
            }
            
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }

            return View(members);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            // To access temp data and store it for persistent access with the keep method
            TempData["User_Id"].ToString();
            TempData.Keep("User_Id");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Member_name,Created_At,Updated_At")] Members members)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To return the allocate the team member to the available space from the database if it exists*/
                    //var team_data = await _context.Boat_Crews.FirstOrDefaultAsync(m => m.Id == 1);
                    var team_data = await _context.Boat_crew_leader
                            .Include(bc => bc.boat_Crew)
                            .FirstOrDefaultAsync(m => m.User_Id == (string)TempData["User_Id"]);
                    team_data.boat_Crew.Boat_crew_allocation -= 1;
                    team_data.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(team_data);
                    _context.Entry(team_data).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation 
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

            if (ModelState.IsValid)
            {
                members.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(members);
                await _context.SaveChangesAsync();
                string userId = TempData["User_Id"].ToString();
                return RedirectToAction(nameof(Index), new { Id = userId} ).WithSuccess("Success", "Successfully Registered member to the team");
            }
            return View(members);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }

            // To access temp data and store it for persistent access with the keep method
            TempData["User_Id"].ToString();
            TempData.Keep("User_Id");

            return View(members);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Member_name,Created_At,Updated_At")] Members members)
        {
            if (id != members.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // To set the system time for record update
                    members.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(members);
                    _context.Entry(members).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersExists(members.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                string userId = TempData["User_Id"].ToString();
                return RedirectToAction(nameof(Index), new { id = userId }).WithSuccess("Success", "Successfully Updated team member details");
            }
            return View(members);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (members == null)
            {
                return NotFound();
            }

            // To access temp data and store it for persistent access with the keep method
            TempData["User_Id"].ToString();
            TempData.Keep("User_Id");

            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var members = await _context.Members.FindAsync(id);
            _context.Members.Remove(members);

            var team_data = await _context.Boat_crew_leader
                            .Include(bc => bc.boat_Crew)
                            .FirstOrDefaultAsync(m => m.User_Id == (string)TempData["User_Id"]);
            team_data.boat_Crew.Boat_crew_allocation += 1;
            team_data.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
            _context.Update(team_data);
            _context.Entry(team_data).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation */

            await _context.SaveChangesAsync();

            // to access the temporary data stored in tempdata variable 
            string userId = TempData["User_Id"].ToString();
            return RedirectToAction(nameof(Index), new { id = userId }).WithSuccess("Success", "Successfully Deleted member from the team");
        }

        private bool MembersExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
