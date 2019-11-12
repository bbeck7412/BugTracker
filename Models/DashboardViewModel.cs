using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class DashboardViewModel
    {
        public List<ProjectViewModel> Projects { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}