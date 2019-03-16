﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Group_I_M32COM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        public AdminController(Data.ApplicationDbContext context)
        {
            _context = context;
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
                           Name = u.FirstName + " " + u.LastName,
                           Email = u.Email,
                           Created_At = u.Created_At,
                           Updated_At = u.Updated_At,
                           Last_Login = u.Last_Login,
                           Login_Status = u.Login_Status
                       };

            return View("~/Views/Admin/Index.cshtml", await data.ToListAsync());
        }
    }
}