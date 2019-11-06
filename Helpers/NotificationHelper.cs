using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class NotificationHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications (Ticket oldTicket, Ticket newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.DeveloperId == null && newTicket.DeveloperId != null;
            var ticketHasBeenUnAssigned = oldTicket.DeveloperId != null && newTicket.DeveloperId == null;
            var ticketHasBeenReassigned = oldTicket.DeveloperId != null && newTicket.DeveloperId != null && oldTicket.DeveloperId != newTicket.DeveloperId;

            if (ticketHasBeenAssigned)
                AddAssignmentNotification(oldTicket, newTicket);
            else if (ticketHasBeenUnAssigned)
                AddUnAssignmentNotification(oldTicket, newTicket);
            else if (ticketHasBeenReassigned)
            {
                AddAssignmentNotification(oldTicket, newTicket);
                AddUnAssignmentNotification(oldTicket, newTicket);
            }

        }

        private void AddAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                RecipientId = newTicket.DeveloperId,
                NotificationBody = $"You have been assigned to Ticket Id {newTicket.Id} on Project {newTicket.Project.Name}. The Ticket Title is {newTicket.Title} and was created on {newTicket.Created} with a priority of {newTicket.TicketPriority.Name}"
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        private void AddUnAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                RecipientId = oldTicket.DeveloperId,
                NotificationBody = $"You have been unassigned from Ticket Id {newTicket.Id} on Project {newTicket.Project.Name}. The Ticket Title is {newTicket.Title} and was created on {newTicket.Created} with a priority of {newTicket.TicketPriority.Name}"
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

    }
}