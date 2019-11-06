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
            return db.TicketStatus.FirstOrDefault(ts => ts.StatusName == "Open").Id;
        }

        public List<Ticket>ListMyTickets()
        {
            var myTickets = new List<Ticket>();
            //Step 1: Determine my role
            var myRole = roleHelper.ListUserRoles(HttpContext.Current.User.Identity.GetUserId()).FirstOrDefault();

            //Step 2: Use that role to build the appropriate set of Tickets
            if (myRole == "Admin")
            {

            }
            else if (myRole == "ProjectManager")
            {

            }
            else if (myRole == "Developer")
            {

            }
            else if (myRole == "Submitter")
            {

            }
            else
            {

            }

            return myTickets;
        }
    }
}