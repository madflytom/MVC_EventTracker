using MVC_EventTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MVC_EventTracker.DAL
{
    public class EventTrackerContext : DbContext
    {

        public EventTrackerContext()
            : base("EventTrackerContext")
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<MVC_EventTracker.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<MVC_EventTracker.Models.Block> Blocks { get; set; }
    }
}