using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_I_M32COM.Data;
using Group_I_M32COM.DbTableModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Group_I_M32COM.Extensions.Alerts;
using Microsoft.AspNetCore.Hosting;

namespace Group_I_M32COM.Controllers
{
    public class BoatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public BoatsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Boats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Boats.ToListAsync());
        }

        // GET: Boats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats
                .Include(b => b.Boat_Medias) // To include the related boat media table with the foreign key
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boat == null)
            {
                return NotFound();
            }
            return View(boat);
        }

        // GET: Boats/Create
        // This action method is responsible to display the create form
        public IActionResult Create()
        {
            // To load the available media options from the database
            var media_data = _context.Boat_Media_Types
                .Select(m => new SelectListItem { Text = m.Boat_media_type_name, Value = m.Id.ToString() })
                .ToList();
            ViewBag.Mediatype = media_data;
            return View();
        }

        // POST: Boats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Binding the image upload to controller through the user Iformfile interface
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Boat_name,Boat_top_speed,Boat_weight,Boat_description,Boat_media_type,Created_At,Updated_At")] Boat boat, List<IFormFile> Selected_files)
        {
            boat.Boat_Medias = new List<Boat_media>();
            // For the image upload to work we needed to add enctype="multipart/form-data" in the form tag.
            foreach (var boat_image in Selected_files)
            {
                if (boat_image.Length > 0)
                {
                    var filename = Path.GetFileName(boat_image.FileName);
                    // Get root path directory
                    var rootpath = Path.Combine(_environment.WebRootPath, "Application_Files\\BoatImages\\");
                    // To check if directory exists. If the directory does not exists we create a new directory
                    if (!Directory.Exists(rootpath))
                    {
                        Directory.CreateDirectory(rootpath);
                    }
                    // Get the path of filename
                    var filepath = Path.Combine(_environment.WebRootPath, "Application_Files\\BoatImages\\", filename);
                    // Copy the image file to target directory path
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await boat_image.CopyToAsync(stream);
                    }

                    // To add the image filepath to the table model property before inserting to the database.
                    boat.Boat_Medias.Add(new Boat_media
                    {
                        Boat_media_url = "~/Application_Files/BoatImages/" + filename,
                        Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                    });
                    boat.Boat_Medias.Count();
                }
            }
            if (ModelState.IsValid)
            {
                // To pass the creation date on system time
                boat.Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                _context.Add(boat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Inserted Boat Details");
            }
            return View(boat);
        }

        // GET: Boats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats.FindAsync(id);
            if (boat == null)
            {
                return NotFound();
            }
            // To load the available media options from the database
            var media_data = await _context.Boat_Media_Types
                .Select(m => new SelectListItem { Text = m.Boat_media_type_name, Value = m.Id.ToString() })
                .ToListAsync();
            ViewBag.Mediatype = media_data;
            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Boat_name,Boat_top_speed,Boat_weight,Boat_description,Boat_media_type,Created_At,Updated_At")] Boat boat)
        {
            if (id != boat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // To set the system time for record update
                    boat.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(boat);
                    _context.Entry(boat).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoatExists(boat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Updated Boat Details");
            }
            return View(boat);
        }

        // GET: Boats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boat == null)
            {
                return NotFound();
            }

            return View(boat);
        }

        // POST: Boats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var boat = await _context.Boats.FindAsync(id);
                    _context.Boats.Remove(boat);
                    var boat_media = _context.Boat_Medias.Where(b => b.Boat.Id == boat.Id);
                    foreach (var boat_image in boat_media)
                    {
                        _context.Boat_Medias.Remove(boat_image);
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
            return RedirectToAction(nameof(Index)).WithSuccess("Success", "Successfully Deleted Boat Details");
        }

        private bool BoatExists(int id)
        {
            return _context.Boats.Any(e => e.Id == id);
        }
    }
}
