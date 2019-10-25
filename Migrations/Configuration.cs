namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            #region Role Creation
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                roleManager.Create(new IdentityRole { Name = "Administrator" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion

            #region User creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];

            if (!context.Users.Any(u => u.Email == "Bbeck7412@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Bbeck7412@gmail.com",
                    Email = "Bbeck7412@gmail.com",
                    FirstName = "Brandon",
                    LastName = "Beck",
                    DisplayName = "Ctrl Z"
                }, "Abc&123");
            }

            if (!context.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin",
                    Email = "DemoAdmin@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "Administrator"
                }, "Abc&123");
            }

            if (!context.Users.Any(u => u.Email == "DemoPM@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoPM",
                    Email = "DemoPM@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Project Manager",
                    DisplayName = "Project Manager"
                }, "Abc&123");
            }

            if (!context.Users.Any(u => u.Email == "DemoDev@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDev",
                    Email = "DemoDev@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "Developer"
                }, "Abc&123");
            }

            if (!context.Users.Any(u => u.Email == "DemoSub@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSub",
                    Email = "DemoSub@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "Submitter"
                }, "Abc&123");
            }
            #endregion

            #region Assign roles

            var adminId = userManager.FindByEmail("Bbeck7412@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var demoAdmin = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(adminId, "Administrator");

            var demoPM = userManager.FindByEmail("DemoPM@mailinator.com").Id;
            userManager.AddToRole(adminId, "ProjectManager");

            var demoDev  = userManager.FindByEmail("DemoDev@mailinator.com").Id;
            userManager.AddToRole(adminId, "Developer");

            var demoSub = userManager.FindByEmail("DemoSub@mailinator.com").Id;
            userManager.AddToRole(adminId, "Submitter");

            #endregion

            #region Assign seeded values
            
            context.Projects.AddOrUpdate(
            p => p.Name,
                new Project { Name = "Brandon's Portfolio", Description = "My Portfolio website", Created = DateTime.Now },
                new Project { Name = "Brandon's Blog", Description = "My Blog Project Using MVC", Created = DateTime.Now },
                new Project { Name = "Brandon's Bug Tracker", Description = "My Bug Tracker Project Using MVC", Created = DateTime.Now }

                );
            context.TicketPriorities.AddOrUpdate(
                p => p.Name,
                new TicketPriority { Name = "Low Priority", Description = "Application or personal procedure unusable, where a workaround is available or a repair is possible." },
                new TicketPriority { Name = "Normal", Description = "Non-critical function or procedure, unusable or hard to use having an operational impact, but with no direct impact on services availability. A workaround is available." },
                new TicketPriority { Name = "Important", Description = "Critical functionality or network access interrupted, degraded or unusable, having a severe impact on services availability. No acceptable alternative is possible." },
                new TicketPriority { Name = "Critical", Description = "Interruption making a critical functionality inaccessible or a complete network interruption causing a severe impact on services availability. There is no possible alternative." }
                );

            context.TicketTypes.AddOrUpdate(
                p => p.Name,
                new TicketType { Name = "Bug", Description = "A bug has been reported in the software." },
                new TicketType { Name = "Feature Request", Description = "A client has requested a new feature to be implemented." },
                new TicketType { Name = "Technical Support", Description = "A client needs assistance using software features." }
                );

            context.TicketStatus.AddOrUpdate(
                p => p.Name,
                new TicketStatus { Name = "Open", Description = "Ticket Creation" },
                new TicketStatus { Name = "Closed", Description = "Ticket has been canceled due to accidental creation or client request." },
                new TicketStatus { Name = "In Progress", Description = "Ticket has been assigned to a team member to be resolved." },
                new TicketStatus { Name = "Resolved", Description = "Ticket has been completed" }
                );


            #endregion
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

    }
}
