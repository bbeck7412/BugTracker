using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Models
{
    public class UserViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int UserId { get; set; }


    }
}