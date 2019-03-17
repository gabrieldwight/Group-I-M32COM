using System;
using System.Collections.Generic;
using System.Text;
using Group_I_M32COM.DbTableModel;
using Group_I_M32COM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group_I_M32COM.Data
{
    // To modify the identityDBContext class extension to identity user, identity role and a generic string
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Declaring Database Table Objects
        public DbSet<Boat> Boats { get; set; }
        public DbSet<Boat_crew> Boat_Crews { get; set; }
        public DbSet<Boat_media> Boat_Medias { get; set; }
        public DbSet<Boat_media_type> Boat_Media_Types { get; set; }
        public DbSet<Boat_type> Boat_Types { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Event_participation> Event_Participations { get; set;}
        public DbSet<Event_type> Event_Types { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Sub_boat_type> Sub_Boat_Types { get; set; }
    }
}
