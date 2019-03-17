using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Group_I_M32COM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Group_I_M32COM.Extensions.Alerts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Group_I_M32COM.Controllers
{
    /* We use the Authroize Data Annotation to assign the role based authorization in AdminController access level
       The authorize data annotation will check if the user is logged and retrieves the user role
       If the user is not logged in it will redirect the user to the login page */
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Instantiate the constructor for the ApplicationDb context
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // To return a list of the users and roles details from the database to the view
        public async Task<IActionResult> AdminIndex()
        {
            var data = from r in _context.Roles
                       join ru in _context.UserRoles on r.Id equals ru.RoleId
                       join u in _context.Users on ru.UserId equals u.Id
                       select new User_RoleModel
                       {
                           Id = r.Id,
                           Role_Name = r.Name,
                           User_Id = ru.UserId,
                           First_Name = u.FirstName,
                           Last_Name = u.LastName,
                           Email = u.Email,
                           Created_At = u.Created_At,
                           Updated_At = u.Updated_At,
                           Last_Login = u.Last_Login,
                           Login_Status = u.Login_Status
                       };

            return View("~/Views/Admin/Index.cshtml", await data.ToListAsync());
        }

        // GET: Admin/SearchUser/string
        // To search the user registered in the database 
        public async Task<IActionResult> SearchUser(string searchString)
        {
            var data = from r in _context.Roles
                       join ru in _context.UserRoles on r.Id equals ru.RoleId
                       join u in _context.Users on ru.UserId equals u.Id
                       select new User_RoleModel
                       {
                           Id = r.Id,
                           Role_Name = r.Name,
                           User_Id = ru.UserId,
                           First_Name = u.FirstName,
                           Last_Name = u.LastName,
                           Email = u.Email,
                           Created_At = u.Created_At,
                           Updated_At = u.Updated_At,
                           Last_Login = u.Last_Login,
                           Login_Status = u.Login_Status
                       };

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(s => s.First_Name.Contains(searchString) || s.Last_Name.Contains(searchString));
            }
            else
            {
                return RedirectToAction(nameof(AdminIndex));
            }
            
            // To check if the record exists in the database
            if (data.Count() != 0)
            {
                return View("~/Views/Admin/Index.cshtml", await data.ToListAsync());
            }
            else
            {
                return RedirectToAction(nameof(AdminIndex)).WithInfo("Not Found", "User does not exist");
            }
        }

        // GET: Admin/Create
        // This action method is responsible to display the create form
        public IActionResult Create()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // To load the available role names from the database
                    var roles_available = _context.Roles
                        .Select(r => new SelectListItem
                        {
                            Text = r.Name,
                            Value = r.Id,
                        })
                        .OrderBy(o => o.Text).ToList();
                    roles_available.Insert(0, new SelectListItem { Text = "Please select role", Value = string.Empty });
                    ViewBag.Roles = roles_available;

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

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Binding the image upload to controller through the user Iformfile interface
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Role_Name,User_Id,First_Name,Last_Name,Email,Address,Created_At,Updated_At")] User_RoleModel user_RoleModel, string Role_Name)
        {
            /* To return the selected application role from the database if it exists*/
            var new_application_role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == Role_Name);

            if (ModelState.IsValid)
            {
                // To pass the creation date on system time
                // The below NETCORE class is used to separate the email string into parts such as user and domain
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(user_RoleModel.Email);
                string email_username = addr.User;

                var user = new ApplicationUser
                {
                    UserName = email_username,
                    Email = user_RoleModel.Email,
                    FirstName = user_RoleModel.First_Name,
                    LastName = user_RoleModel.Last_Name,
                    Address = user_RoleModel.Address,
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                };

                string password = "Admin_1";

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // Add role to user after successful insert
                    result = await _userManager.AddToRoleAsync(user, new_application_role.Name);
                }
                return RedirectToAction(nameof(AdminIndex)).WithSuccess("Success", "Successfully Inserted User Details");
            }
            return View(user_RoleModel);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // To get the select user detail from the passed Id parameter variable 
            var data = from r in _context.Roles
                       join ru in _context.UserRoles on r.Id equals ru.RoleId
                       join u in _context.Users on ru.UserId equals u.Id
                       where u.Id == id
                       select new User_RoleModel
                       {
                           Id = r.Id,
                           Role_Name = r.Name,
                           User_Id = ru.UserId,
                           First_Name = u.FirstName,
                           Last_Name = u.LastName,
                           Email = u.Email,
                           Address = u.Address,
                           Created_At = u.Created_At,
                           Updated_At = u.Updated_At,
                           Last_Login = u.Last_Login,
                           Login_Status = u.Login_Status
                       };

            if (data == null)
            {
                return NotFound();
            }

            return View(await data.SingleAsync());
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = from r in _context.Roles
                       join ru in _context.UserRoles on r.Id equals ru.RoleId
                       join u in _context.Users on ru.UserId equals u.Id
                       where u.Id == id
                       select new User_RoleModel
                       {
                           Id = r.Id,
                           Role_Name = r.Name,
                           User_Id = ru.UserId,
                           First_Name = u.FirstName,
                           Last_Name = u.LastName,
                           Email = u.Email,
                           Address = u.Address,
                           Created_At = u.Created_At,
                           Updated_At = u.Updated_At,
                           Last_Login = u.Last_Login,
                           Login_Status = u.Login_Status
                       };

            if (data == null)
            {
                return NotFound();
            }

            // Temporary Variable to assign the role object from the joined tables
            var user_roles = await data.SingleAsync();

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // To load the available role names from the database
                    var roles_available = _context.Roles
                        .Select(r => new SelectListItem
                        {
                            Text = r.Name,
                            Value = r.Id,
                            Selected = r.Id == user_roles.Id ? true : false
                        })
                        .OrderBy(o => o.Text).ToList();
                    roles_available.Insert(0, new SelectListItem { Text = "Please select role", Value = string.Empty });
                    ViewBag.Roles = roles_available;

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
            return View(await data.SingleAsync());
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Role_Name,User_Id,First_Name,Last_Name,Email,Address,Created_At,Updated_At")] User_RoleModel user_RoleModel, string Role_Name)
        {
            if (id != user_RoleModel.User_Id)
            {
                return NotFound();
            }

            /* To return the selected application user and application role from the database if it exists*/
            var applicationuser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            var new_application_role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == Role_Name);
            var old_application_role = await _context.UserRoles.SingleOrDefaultAsync(ru => ru.UserId == applicationuser.Id);
            var old_RoleName = await _context.Roles.SingleOrDefaultAsync(r => r.Id == old_application_role.RoleId);

            if (ModelState.IsValid)
            {
                try
                {
                    // To set the system time for record update
                    applicationuser.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                    _context.Update(applicationuser);
                    _context.Entry(applicationuser).Property(x => x.Created_At).IsModified = false; // To prevent the datetime property to be set as null on update operation 

                    
                    if (old_application_role.RoleId != Role_Name)
                    {
                        await _userManager.RemoveFromRoleAsync(applicationuser, old_RoleName.Name);
                        await _userManager.AddToRoleAsync(applicationuser, new_application_role.Name);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationuser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminIndex)).WithSuccess("Success", "Successfully Updated User Details");
            }
            return View(user_RoleModel);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // To get the select user detail from the passed Id parameter variable 
            var user = from r in _context.Roles
                       join ru in _context.UserRoles on r.Id equals ru.RoleId
                       join u in _context.Users on ru.UserId equals u.Id
                       where u.Id == id
                       select new User_RoleModel
                       {
                           Id = r.Id,
                           Role_Name = r.Name,
                           User_Id = ru.UserId,
                           First_Name = u.FirstName,
                           Last_Name = u.LastName,
                           Email = u.Email,
                           Address = u.Address,
                           Created_At = u.Created_At,
                           Updated_At = u.Updated_At,
                           Last_Login = u.Last_Login,
                           Login_Status = u.Login_Status
                       };

            if (user == null)
            {
                return NotFound();
            }

            return View(await user.SingleAsync());
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationuser = new ApplicationUser();
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    /* To return the selected application user from the database if it exists*/
                    applicationuser = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

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

            if (applicationuser != null)
            {
                await _userManager.DeleteAsync(applicationuser);
            }
            return RedirectToAction(nameof(AdminIndex)).WithSuccess("Success", "Successfully Deleted User Details");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}