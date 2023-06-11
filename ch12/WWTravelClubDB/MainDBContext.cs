using System;
using Microsoft.EntityFrameworkCore;
using WWTravelClubDB.Models;

namespace WWTravelClubDB
{
    public class MainDBContext: DbContext
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public MainDBContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Destination>()
                .OwnsMany(m => m.Packages);      
        }   
    }
}
