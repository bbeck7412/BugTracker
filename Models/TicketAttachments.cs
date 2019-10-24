using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTime Created { set; get; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        //Virtual Nav...

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}