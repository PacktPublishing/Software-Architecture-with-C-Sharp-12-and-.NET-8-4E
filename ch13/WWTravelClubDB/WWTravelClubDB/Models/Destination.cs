using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWTravelClubDB.Models
{
    //[EntityTypeConfiguration(typeof(DestinationConfiguration))]
    public class Destination
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public required string Name { get; set; }
        [MaxLength(128)]
        public required string Country { get; set; }
        public string? Description { get; set; }
        public ICollection<Package> Packages { get; set; } = null!;
    }
}
