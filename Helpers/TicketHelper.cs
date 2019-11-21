using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class TicketHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        public int SetDefaultTicketStatus()
        {
            return db.TicketStatus.FirstOrDefault(ts => ts.Name == "New").Id;
        }

        public List<Ticket>ListMyTickets()
        {
            var myTickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            //Step 1: Determine my role
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            ////Step 2: Use that role to build the appropriate set of Tickets
            ///
            /// If statement example 
            /// 
            //if (myRole == "Admin")
            //{
            //    myTickets.AddRange(db.Tickets);
            //}
            //else if (myRole == "ProjectManager")
            //{

            //    myTickets.AddRange(user.Projects.SelectMany(p => p.Tickets));
            //}
            //else if (myRole == "Developer")
            //{
            //    myTickets.AddRange(db.Tickets.Where(t => t.DeveloperId == userId));
            //}
            //else if (myRole == "Submitter")
            //{
            //    myTickets.AddRange(db.Tickets.Where(t => t.SubmitterId == userId));
            //}

            switch(myRole)
            {
                case "Admin":
                case "Administrator":
                    myTickets.AddRange(db.Tickets);
                 break;

                case
                "ProjectManager":
                    myTickets.AddRange(user.Projects.SelectMany(p => p.Tickets));
                break;

                case
                "Developer":
                    myTickets.AddRange(db.Tickets.Where(t => t.DeveloperId == userId));
                break;

                case 
                "Submitter":
                    myTickets.AddRange(db.Tickets.Where(t => t.SubmitterId == userId));
                break;
                    
            }


            return myTickets;
        }
    }
}