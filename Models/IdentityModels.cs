﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(Name ="LastName")]
        public string LastName { get; set; }
        [Display(Name ="DisplayName")]
        public string DisplayName { get; set; }

        public virtual ICollection <TicketComment> TicketComments { get; set; }
        public virtual ICollection <Project> Projects { get; set; }
        public virtual ICollection <TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection <TicketHistory> TicketHistories { get; set; }
        public virtual ICollection <TicketNotification> TicketNotifications { get; set; }

        public ApplicationUser()
        {
            TicketComments = new HashSet<TicketComment>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
            Projects = new HashSet<Project>();

        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
           

        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
}

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }
}
}