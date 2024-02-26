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
                .HasMany(m => m.Packages)
                .WithOne(m => m.MyDestination)
                .HasForeignKey(m => m.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Package>()
            //    .HasOne(m => m.MyDestination)
            //    .WithMany(m => m.Packages)
            //    .HasForeignKey(m => m.DestinationId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Destination>()
                .HasIndex(m => m.Country);

            builder.Entity<Destination>()
                .HasIndex(m => m.Name);

            builder.Entity<Package>()
                .HasIndex(m => m.Name);

            builder.Entity<Package>()
                .HasIndex("StartValidityDate", "EndValidityDate");
        }   
    }
}
