using BugTracker.Helpers;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult DemoLogin ()
        {
            return View ();
        }



        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Setting up project View Models  
                List<ProjectViewModel> prjVM = new List<ProjectViewModel>();
                var projects = db.Projects.ToList();
                foreach (var prj in projects)
                {
                    ProjectViewModel vm = new ProjectViewModel();
                    vm.Project = prj;
                    var pmId = db.Roles.FirstOrDefault(p => p.Name == "ProjectManager").Id;
                    vm.ProjectManager = db.Projects.Find(prj.Id).Users.FirstOrDefault(u => u.Roles.Any(r => r.RoleId == pmId));
                    prjVM.Add(vm);
                }


                //Setting up dashboard view model 
                var model = new DashboardViewModel();
                model.Projects = prjVM;
                model.Tickets = db.Tickets.ToList();
                model.Users = db.Users.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await EmailHelper.ComposeEmailAsync(model);
                    return View("Index", "Home");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult ProfilePage()
        {
            return RedirectToAction("ProfilePage","Home");
        }

    }

}