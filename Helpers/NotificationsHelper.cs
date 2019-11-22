using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class NotificationsHelper
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.DeveloperId == null && newTicket.DeveloperId != null;
            var ticketHasBeenUnAssigned = oldTicket.DeveloperId != null && newTicket.DeveloperId == null;
            var ticketHasBeenReAssigned = oldTicket.DeveloperId != null && newTicket.DeveloperId != null && oldTicket.DeveloperId != newTicket.DeveloperId;

            if (ticketHasBeenAssigned)
                AddAssignmentNotification(oldTicket, newTicket);
            else if (ticketHasBeenUnAssigned)
                AddUnAssignmentNotification(oldTicket, newTicket);
            else if (ticketHasBeenReAssigned)
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
                NotificationBody = $"You have been assigned to a ticket {newTicket.Id} on {newTicket.Project.Name}. The ticket title is {newTicket.Title} and was created on {newTicket.Created} with a priority of {newTicket.TicketPriority.Name}"

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
                NotificationBody = $"You have been Unassigned from a ticket {newTicket.Id} on {newTicket.Project.Name}. The ticket title is {newTicket.Title} and was created on {newTicket.Created} with a priority of {newTicket.TicketPriority.Name}"

            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

    }
}