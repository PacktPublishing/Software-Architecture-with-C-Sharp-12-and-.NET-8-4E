using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using PackagesManagementDomain.Aggregates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PackagesManagementDB.Models
{
    //[EntityTypeConfiguration(typeof(DestinationConfiguration))]
    public class Destination : Entity<int>, IDestination
    {
        
        [MaxLength(128)]
        public string Name { get; set; } = null!;
        [MaxLength(128)]
        public string Country { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Package> Packages { get; set; } = null!;
        public void FullUpdate(IDestination o)
        {
            if (IsTransient())
            {
                Id = o.Id;
            }
            Name = o.Name;
            Country = o.Country;
            Description = o.Description;
        }
    }
}
