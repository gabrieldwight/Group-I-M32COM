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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Group_I_M32COM.Controllers
{
    /* We use the Authroize Data Annotation to assign the role based authorization in controller access level
      The authorize data annotation will check if the user is logged and retrieves the user role
      If the user is not logged in it will redirect the user to the login page */
    [Authorize(Roles = "Admin")]
    public class Boat_crewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public Boat_crewController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Boat_crew
        public async Task<IActionResult> Index()
        {
            return View(await _context.Boat_Crews.ToListAsync());
        }

        // GET: Boat_crew/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat_crew = await _context.Boat_Crews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boat_crew == null)
            {
                return NotFound();
            }

            return View(boat_crew);
        }

        // GET: Boat_crew/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boat_crew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Boat_crew_name,Boat_crew_address,Boat_crew_phone,Boat_crew_logo,Boat_crew_allocation,Created_At,Updated_At")] Boat_crew boat_crew, List<IFormFile> Selected_files)
        {
            // For the image upload to work we needed to add enctype="multipart/form-data" in the form tag.
            if (Selected_files != null)
            {
                foreach (var boat_image in Selected_files)
                {
                    if (boat_image.Length > 0)
                    {
                        var filename = Path.GetFileName(boat_image.FileName);
                        // Get root path directory
                        var rootpath = Path.Combine(_environment.WebRootPath, "Application_Files\\BoatTeamImages\\");
                        // To check if directory exists. If the directory does not exists we create a new directory
                        if (!Directory.Exists(rootpath))
                        {
                            Directory.CreateDirectory(rootpath);
                        }
                        // Get the path of filename
                        var filepath = Path.Combine(_environment.WebRootPath, "Application_Files\\BoatTeamImages\\", filename);
                        // Copy the image file to target directory path
                        using (var stream = new FileStream(filepath, FileMode.Create))
                        {
                            await boat_image.CopyToAsync(stream);
                        }

                        // To add the number of image filepath to the table model property before inserting to the database.
                        boat_crew.Boat_crew_logo = "~/Application_Files/BoatTeamImages/" + filename;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                boat_crew.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(boat_crew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Inserted Boat Team Details");
            }
            return View(boat_crew);
        }

        // GET: Boat_crew/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat_crew = await _context.Boat_Crews.FindAsync(id);
            if (boat_crew == null)
            {
                return NotFound();
            }
            return View(boat_crew);
        }

        // POST: Boat_crew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Boat_crew_name,Boat_crew_address,Boat_crew_phone,Boat_crew_logo,Boat_crew_allocation,Created_At,Updated_At")] Boat_crew boat_crew)
        {
            if (id != boat_crew.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    boat_crew.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(boat_crew);
                    _context.Entry(boat_crew).Property(x => x.Created_At).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Boat_crewExists(boat_crew.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully updated Boat Team Details");
            }
            return View(boat_crew);
        }

        // GET: Boat_crew/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat_crew = await _context.Boat_Crews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boat_crew == null)
            {
                return NotFound();
            }

            return View(boat_crew);
        }

        // POST: Boat_crew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boat_crew = await _context.Boat_Crews.FindAsync(id);
            _context.Boat_Crews.Remove(boat_crew);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully deleted Boat Team Details");
        }

        private bool Boat_crewExists(int id)
        {
            return _context.Boat_Crews.Any(e => e.Id == id);
        }
    }
}
