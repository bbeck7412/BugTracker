using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class TicketHistoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void RecordHistoricalChanges(Ticket oldTicket, Ticket newTicket)
        {
            //Compare a few properties from the old and new ticket to determine if changes were made that I care about.
            if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketStatusId",
                    OldValue = oldTicket.TicketStatus.StatusName,
                    NewValue = newTicket.TicketStatus.StatusName,
                    Changed = (DateTime)newTicket.Updated,
                    TicketId = newTicket.Id,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketPriorityId",
                    OldValue = oldTicket.TicketPriority.Name,
                    NewValue = newTicket.TicketPriority.Name,
                    Changed = (DateTime)newTicket.Updated,
                    TicketId = newTicket.Id,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.DeveloperId != newTicket.DeveloperId)
            {

                var newHistoryRecord = new TicketHistory
                {
                    Property = "DeveloperId",
                    OldValue = oldTicket.Developer == null? "Un-assigned" : oldTicket.Developer.DisplayName,
                    NewValue = newTicket.Developer == null? "Un-assigned" : newTicket.Developer.DisplayName,
                    Changed = (DateTime)newTicket.Updated,
                    TicketId = newTicket.Id,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.TicketHistories.Add(newHistoryRecord);
            }

            db.SaveChanges();
        }
    }
}