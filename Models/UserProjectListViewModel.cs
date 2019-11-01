using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class UserProjectListViewModel
    {
        public string Name { get; set; }
        public List<string> ProjectNames { get; set; }

        public UserProjectListViewModel()
        {
            ProjectNames = new List<string>();
        }
    }
}