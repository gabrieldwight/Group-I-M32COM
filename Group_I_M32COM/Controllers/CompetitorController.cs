using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_I_M32COM.Data;
using Group_I_M32COM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Group_I_M32COM.Controllers
{
    public class CompetitorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitorController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Competitor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Competitor/Details/5
        public ActionResult Details(string id)
        {
            string user_id = id.ToString();
            var applicationUser = from r in _context.Roles
                                  join ru in _context.UserRoles on r.Id equals ru.RoleId
                                  join u in _context.Users on ru.UserId equals u.Id
                                  where u.Id == user_id
                                  select new User_RoleModel
                                  {
                                    Role_Name = r.Name,
                                    First_Name = u.FirstName,
                                    Last_Name = u.LastName,
                                    Address = u.Address,
                                    Email= u.Email,                                       
                                  };
                                  
            return View(applicationUser.Single());
        }

        // GET: Competitor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Competitor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Competitor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Competitor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Competitor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Competitor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}