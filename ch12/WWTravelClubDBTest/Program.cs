using System;
using System.Collections.Generic;
using WWTravelClubDB;
using WWTravelClubDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace WWTravelClubDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("program start: populate database");
            Console.ReadKey();

            var context = new LibraryDesignTimeDbContextFactory()
                .CreateDbContext();
            context.Database.EnsureCreated();
            var firstDestination = new Destination
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Florence",
                Country = "Italy",
                Packages = new List<Package>()
            {
                new Package
                {
                    Id=Guid.NewGuid().ToString(),
                    Name = "Summer in Florence",
                    StartValidityDate = new DateTime(2019, 6, 1),
                    EndValidityDate = new DateTime(2019, 10, 1),
                    DurationInDays=7,
                    Price=1000
                },
                new Package
                {
                    Id=Guid.NewGuid().ToString(),
                    Name = "Winter in Florence",
                    StartValidityDate = new DateTime(2019, 12, 1),
                    EndValidityDate = new DateTime(2020, 2, 1),
                    DurationInDays=7,
                    Price=500
                }
            }
            };
            context.Destinations.Add(firstDestination);
            context.SaveChanges();
            Console.WriteLine(
                "DB populated: first destination id is " +
                firstDestination.Id);
            Console.ReadKey();

            var toModify = context.Destinations
                .Where(m => m.Name == "Florence")
                .FirstOrDefault();

            toModify.Description = 
                            "Florence is a famous historical Italian town";
            foreach (var package in toModify.Packages)
                            package.Price = package.Price * 1.1m;
            context.SaveChanges();

            var verifyChanges= context.Destinations
                    .Where(m => m.Name == "Florence")
                    .FirstOrDefault();

            Console.WriteLine(
                "New Florence description: " +
                verifyChanges.Description);
            Console.ReadKey();
            var period = new DateTime(2019, 8, 10);
            var list = context.Destinations
                .AsEnumerable()
                .SelectMany(m => m.Packages)
                .Where(m => period >= m.StartValidityDate
                && period <= m.EndValidityDate)
                .Select(m => new PackagesListDTO
                {
                    StartValidityDate=m.StartValidityDate,
                    EndValidityDate=m.EndValidityDate,
                    Name=m.Name,
                    DurationInDays=m.DurationInDays,
                    Id=m.Id,
                    Price=m.Price,
                    DestinationName=m.MyDestination.Name
                })
                .ToList();
            foreach (var result in list)
                Console.WriteLine(result.ToString());
            Console.ReadKey();
            
        }
        
    }
}
