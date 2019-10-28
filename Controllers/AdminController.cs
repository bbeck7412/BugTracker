using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult ManageRoles()
        {
            ViewBag.Users = new MultiSelectList(db.Users, "Id","Email");
            ViewBag.Role = new SelectList(db.Roles,"Name","Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles()
        {
            return View();
        }
    }
}