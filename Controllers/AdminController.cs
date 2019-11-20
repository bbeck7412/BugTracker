using BugTracker.Helpers;
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
        private RoleHelper roleHelper = new RoleHelper();
        private ProjectHelper projectHelper = new ProjectHelper();

        [Authorize(Roles = "Admin,Administrator")]
        // GET: Admin
        public ActionResult ManageRoles()
        {
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id","DisplayName");
            ViewBag.Role = new SelectList(db.Roles,"Name","Name");

            var users = new List<ManageRolesViewModel>();
            foreach (var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    UserName = $"{user.FirstName},{user.LastName}",
                    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault()
                });
            }

            return View(users);
        }

        [Authorize(Roles = "Admin,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string role)
        {
            //Unenroll all the selected users from occupied roles
            foreach(var userId in userIds)
            {
                //What is the role?
                var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                if(userRole != null)
                {
                    roleHelper.RemoveUserFromRole(userId,userRole);
                }

            }
            // Add them to selected Role

            if (!string.IsNullOrEmpty(role))
            {
                foreach (var userId in userIds)
                {
                    roleHelper.AddUserToRole(userId, role);
                }
            }
            return RedirectToAction("ManageRoles","Admin");
        }


        [Authorize(Roles = "Admin,Administrator,ProjectManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(List<int> projects, string projectManagerId, List <string> developers, List <string> submitters)
        {

            if(projects != null)
            {
                foreach (var projectId in projects)
                {
                    //Remove everyone from this project
                    foreach (var user in projectHelper.UsersOnProject(projectId).ToList())
                    {
                        projectHelper.RemoveUserFromProject(user.Id, projectId);
                    }
                    //Add back PM if possible
                    if (!string.IsNullOrEmpty(projectManagerId))
                    {
                        projectHelper.AddUserToProject(projectManagerId, projectId);

                    }

                    if (developers != null)
                    {
                        foreach(var developerId in developers)
                        {
                            projectHelper.AddUserToProject(developerId, projectId);
                        }
                    }

                    if (submitters != null)
                    {
                        foreach (var submitterId in submitters)
                        {
                            projectHelper.AddUserToProject(submitterId, projectId);
                        }
                    }
                    
                }
            }

            return RedirectToAction("ManageProjectUsers");
        }


        [Authorize(Roles = "Admin, ProjectManager, Administrator")]
        [HttpGet]
        
        public ActionResult ManageProjectUsers()
        {
            ViewBag.Projects = new MultiSelectList(db.Projects, "Id", "Name");
            ViewBag.Developers = new MultiSelectList(roleHelper.UsersInRole("Developer"), "Id", "DisplayName");
            ViewBag.Submitters = new MultiSelectList(roleHelper.UsersInRole("Submitter"), "Id", "DisplayName");

            if (User.IsInRole("Admin") | User.IsInRole("Administrator"))
            {
                ViewBag.ProjectManagerId = new SelectList(roleHelper.UsersInRole("ProjectManager"), "Id", "DisplayName");
            }


            var myData = new List<UserProjectListViewModel>();
            UserProjectListViewModel userVm = null;
            foreach(var user in db.Users.ToList())
            {
                userVm = new UserProjectListViewModel
                {
                    Name = $"{user.FirstName}, {user.LastName}",
                    ProjectNames = projectHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList()
                };

                if (userVm.ProjectNames.Count() == 0)
                    userVm.ProjectNames.Add("N/A");

                myData.Add(userVm);
            }

            return View(myData);

        }


    }
}