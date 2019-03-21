using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_I_M32COM.Data;
using Group_I_M32COM.DbTableModel;
using static Group_I_M32COM.Helpers.Data_RolesEnum;
using Microsoft.AspNetCore.Authorization;
using Group_I_M32COM.Models;

namespace Group_I_M32COM.Controllers
{
    /* We use the Authroize Data Annotation to assign the role based authorization in EventsController access level
       The authorize data annotation will check if the user is logged and retrieves the user role
       If the user is not logged in it will redirect the user to the login page */
    [Authorize(Roles = "Admin")]
    public class Boat_crew_leaderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Boat_crew_leaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boat_crew_leader
        public async Task<IActionResult> Index()
        {
            // To get the name of the user assigned to the boat team related with the user id
            var applicationUser = from bc in _context.Boat_crew_leader
                                  join u in _context.Users on bc.User_Id equals u.Id
                                  select new Boat_CrewLeaderViewModel
                                  {
                                      Id = bc.Id,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      TeamName = bc.boat_Crew.Boat_crew_name,
                                      Created_At = bc.Created_At,
                                      Updated_At = bc.Updated_At
                                  };


            return View(await applicationUser.ToListAsync());
        }

        // GET: Boat_crew_leader/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var boat_crew_leader = await _context.Boat_crew_leader
                .FirstOrDefaultAsync(m => m.Id == id);*/

            // To get the name of the user assigned to the boat team related with the user id
            var boat_crew_leader = from bc in _context.Boat_crew_leader
                                  join u in _context.Users on bc.User_Id equals u.Id
                                  where bc.Id == id
                                  select new Boat_CrewLeaderViewModel
                                  {
                                      Id = bc.Id,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      TeamName = bc.boat_Crew.Boat_crew_name,
                                      Created_At = bc.Created_At,
                                      Updated_At = bc.Updated_At
                                  };

            if (boat_crew_leader == null)
            {
                return NotFound();
            }

            return View(await boat_crew_leader.SingleAsync());
        }

        // GET: Boat_crew_leader/Create
        public IActionResult Create()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To retrieve the users available in the User table and display the available records 
                       in the dropdown list*/
                    var get_boat_team_registered = _context.Boat_crew_leader.ToList();


                    var applicationUser = from r in _context.Roles
                                          join ru in _context.UserRoles on r.Id equals ru.RoleId
                                          join u in _context.Users on ru.UserId equals u.Id
                                          where r.Name == Role_Enum.TeamLeader.ToString() && get_boat_team_registered.Any(x => x.User_Id != u.Id)
                                          select new SelectListItem
                                          {
                                              Text = u.FirstName + " " + u.LastName,
                                              Value = u.Id.ToString()
                                          };

                    var user = applicationUser.ToList();
                    user.Insert(0, new SelectListItem { Text = "Select User", Value = string.Empty });
                    ViewBag.User = user;

                    /* To retrieve the boats crew available in the boat crew table and display the available records 
                       in the dropdown list*/
                    var boat_crew = _context.Boat_Crews
                        .Where(x => get_boat_team_registered.Any(y => y.boat_Crew.Id != x.Id))
                        .Select(e => new SelectListItem { Text = e.Boat_crew_name, Value = e.Id.ToString() })
                        .ToList();
                    boat_crew.Insert(0, new SelectListItem { Text = "Select Boat Crew", Value = string.Empty });
                    ViewBag.BoatCrew = boat_crew;

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

        // POST: Boat_crew_leader/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User_Id,Created_At,Updated_At")] Boat_crew_leader boat_crew_leader, string boat_Crew)
        {
            /* To return the selected boat team name from the database if it exists*/
            var get_boat_team = _context.Boat_Crews.SingleOrDefault(x => x.Id == Convert.ToInt32(boat_Crew));
            boat_crew_leader.boat_Crew = get_boat_team;

            if (ModelState.IsValid)
            {
                // To pass the creation date on system time
                boat_crew_leader.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(boat_crew_leader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boat_crew_leader);
        }

        // GET: Boat_crew_leader/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var boat_crew_leader = await _context.Boat_crew_leader.FindAsync(id);
            var boat_crew_leader = await _context.Boat_crew_leader
                .Include(bt => bt.boat_Crew)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (boat_crew_leader == null)
            {
                return NotFound();
            }

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To retrieve the users available in the User table and display the available records 
                       in the dropdown list*/

                    var applicationUser = from r in _context.Roles
                                          join ru in _context.UserRoles on r.Id equals ru.RoleId
                                          join u in _context.Users on ru.UserId equals u.Id
                                          where r.Name == Role_Enum.TeamLeader.ToString() && u.Id == boat_crew_leader.User_Id
                                          select new SelectListItem
                                          {
                                              Text = u.FirstName + " " + u.LastName,
                                              Value = u.Id.ToString(),
                                              Selected = u.Id == boat_crew_leader.User_Id ? true : false
                                          };

                    var user = applicationUser.ToList();
                    user.Insert(0, new SelectListItem { Text = "Select User", Value = string.Empty });
                    ViewBag.User = user;

                    /* To retrieve the boats crew available in the boat crew table and display the available records 
                       in the dropdown list*/

                    var boat_crew = _context.Boat_Crews
                        .Select(e => new SelectListItem
                        {
                            Text = e.Boat_crew_name,
                            Value = e.Id.ToString(),
                            Selected = e.Id == boat_crew_leader.boat_Crew.Id ? true : false
                        })
                        .ToList();
                    boat_crew.Insert(0, new SelectListItem { Text = "Select Boat Crew", Value = string.Empty });
                    ViewBag.BoatCrew = boat_crew;

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

            return View(boat_crew_leader);
        }

        // POST: Boat_crew_leader/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User_Id,Created_At,Updated_At")] Boat_crew_leader boat_crew_leader, string boat_Crew)
        {
            if (id != boat_crew_leader.Id)
            {
                return NotFound();
            }

            /* To return the selected boat team name from the database if it exists*/
            var get_boat_team = _context.Boat_Crews.SingleOrDefault(x => x.Id == Convert.ToInt32(boat_Crew));
            boat_crew_leader.boat_Crew = get_boat_team;

            if (ModelState.IsValid)
            {
                try
                {
                    // To set the system time for record update
                    boat_crew_leader.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(boat_crew_leader);
                    _context.Entry(boat_crew_leader).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Boat_crew_leaderExists(boat_crew_leader.Id))
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
            return View(boat_crew_leader);
        }

        // GET: Boat_crew_leader/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var boat_crew_leader = await _context.Boat_crew_leader
                .FirstOrDefaultAsync(m => m.Id == id);*/

            // To get the name of the user assigned to the boat team related with the user id
            var boat_crew_leader = from bc in _context.Boat_crew_leader
                                   join u in _context.Users on bc.User_Id equals u.Id
                                   where bc.Id == id
                                   select new Boat_CrewLeaderViewModel
                                   {
                                       Id = bc.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       TeamName = bc.boat_Crew.Boat_crew_name,
                                       Created_At = bc.Created_At,
                                       Updated_At = bc.Updated_At
                                   };

            if (boat_crew_leader == null)
            {
                return NotFound();
            }

            return View(await boat_crew_leader.SingleAsync());
        }

        // POST: Boat_crew_leader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boat_crew_leader = await _context.Boat_crew_leader.FindAsync(id);
            _context.Boat_crew_leader.Remove(boat_crew_leader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Boat_crew_leaderExists(int id)
        {
            return _context.Boat_crew_leader.Any(e => e.Id == id);
        }
    }
}
