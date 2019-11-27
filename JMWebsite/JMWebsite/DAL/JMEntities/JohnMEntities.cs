using JMWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace JMWebsite.DAL.JMEntities
{
    public class JohnMEntities : DbContext
    {
        public JohnMEntities() : base("name=JohnMEntities")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<CateringService> CateringServices { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventImage> EventImages { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Event>().HasOptional(e => e.cateringService).WithRequired(c => c._event);
            modelBuilder.Entity<Event>().HasOptional(e => e.schedule).WithRequired(s => s._event);
            modelBuilder.Entity<Event>().HasOptional(e => e.contract).WithRequired(c => c._event);
            modelBuilder.Entity<Schedule>().HasRequired(s => s._event);
            modelBuilder.Entity<CateringService>().HasRequired(s => s._event);
            modelBuilder.Entity<Contract>().HasRequired(s => s._event);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}