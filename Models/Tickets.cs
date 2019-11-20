using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        public string SubmitterId {get;set;}
        public string DeveloperId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public DateTime Created { set; get; }
        public DateTime? Updated { get; set; }

        //Nav Section... 
        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual ApplicationUser Submitter { get; set; }
        public virtual ApplicationUser Developer { get; set; }

       
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        public Ticket ()
        {
           
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketNotifications = new HashSet<TicketNotification>();
            
        }
        
    }
}