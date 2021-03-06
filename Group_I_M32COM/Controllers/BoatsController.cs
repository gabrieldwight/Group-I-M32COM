﻿using System;
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
using Microsoft.AspNetCore.Authorization;

namespace Group_I_M32COM.Controllers
{
    /* We use the Authroize Data Annotation to assign the role based authorization in BoatsController access level
       The authorize data annotation will check if the user is logged and retrieves the user role
       If the user is not logged in it will redirect the user to the login page */
    [Authorize(Roles = "Admin")]
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
            // To read related data from the boat_type table and sub_boat_type table to display Boat details
            var _boats = _context.Boats
                .Include(boat_category => boat_category.Boat_Types)
                .Include(sub_boat_category => sub_boat_category.Sub_Boat_Types);
            return View(await _boats.ToListAsync());
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
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // To load the available media options from the database
                    var media_data = _context.Boat_Media_Types
                        .Select(m => new SelectListItem { Text = m.Boat_media_type_name, Value = m.Id.ToString() })
                        .ToList();
                    media_data.Insert(0, new SelectListItem { Text = "Please select media category", Value = string.Empty });
                    ViewBag.Mediatype = media_data;

                    // To load the boat class category
                    var sub_boat_category_data = _context.Sub_Boat_Types
                        .Select(c => new SelectListItem { Text = c.Sub_boat_class_type, Value = c.Id.ToString() })
                        .ToList();
                    sub_boat_category_data.Insert(0, new SelectListItem { Text = "Please select sub boat class category", Value = string.Empty });
                    ViewBag.Sub_Boat_Category_type = sub_boat_category_data;

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

        // POST: Boats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Binding the image upload to controller through the user Iformfile interface
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Boat_name,Boat_top_speed,Boat_weight,Boat_description,Boat_media_type,Created_At,Updated_At")] Boat boat, List<IFormFile> Selected_files, string Sub_Boat_Types)
        {
            boat.Boat_Medias = new List<Boat_media>();
            // For the image upload to work we needed to add enctype="multipart/form-data" in the form tag.
            if (Selected_files != null)
            {
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

                        // To add the number of image filepath to the table model property before inserting to the database.
                        boat.Boat_Medias.Add(new Boat_media
                        {
                            Boat_media_url = "~/Application_Files/BoatImages/" + filename,
                            Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                        });
                        boat.Boat_Medias.Count();
                    }
                }
            }

            /* To return the selected boat_type class and sub boat_type class from the database if it exists*/
            var get_boat_type = _context.Sub_Boat_Types
                .Include(boat_parent_category => boat_parent_category.Boat_Types)
                .SingleOrDefault(x => x.Id == Convert.ToInt32(Sub_Boat_Types));
            boat.Sub_Boat_Types = get_boat_type;
            boat.Boat_Types = get_boat_type.Boat_Types;

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

            //var boat = await _context.Boats.FindAsync(id);
            var boat = await _context.Boats
                .Include(sb => sb.Sub_Boat_Types)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (boat == null)
            {
                return NotFound();
            }

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // To load the available media options from the database
                    var media_data = _context.Boat_Media_Types
                        .Select(m => new SelectListItem
                        {
                            Text = m.Boat_media_type_name,
                            Value = m.Id.ToString(),
                            Selected = m.Id == boat.Boat_media_type ? true : false
                        })
                        .ToList();
                    media_data.Insert(0, new SelectListItem { Text = "Please select media category", Value = string.Empty });
                    ViewBag.Mediatype = media_data;

                    // To load the boat class category
                    var sub_boat_category_data = _context.Sub_Boat_Types
                       .Select(c => new SelectListItem
                       {
                           Text = c.Sub_boat_class_type,
                           Value = c.Id.ToString(),
                           Selected = c.Id == boat.Sub_Boat_Types.Id ? true : false
                       })
                       .ToList();
                    sub_boat_category_data.Insert(0, new SelectListItem { Text = "Please select sub boat class category", Value = string.Empty });
                    ViewBag.Sub_Boat_Category_type = sub_boat_category_data;
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
            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Boat_name,Boat_top_speed,Boat_weight,Boat_description,Boat_media_type,Created_At,Updated_At")] Boat boat, string Sub_Boat_Types)
        {
            if (id != boat.Id)
            {
                return NotFound();
            }

            /* To return the selected boat_type class and sub boat_type class from the database if it exists*/
            var get_boat_type = _context.Sub_Boat_Types
                .Include(boat_parent_category => boat_parent_category.Boat_Types)
                .SingleOrDefault(x => x.Id == Convert.ToInt32(Sub_Boat_Types));
            boat.Sub_Boat_Types = get_boat_type;
            boat.Boat_Types = get_boat_type.Boat_Types;

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
                    // Delete the boat record from the boat table
                    var boat = await _context.Boats.FindAsync(id);
                    _context.Boats.Remove(boat);

                    // To get the related boat ID Foreign key from the boat media table
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
