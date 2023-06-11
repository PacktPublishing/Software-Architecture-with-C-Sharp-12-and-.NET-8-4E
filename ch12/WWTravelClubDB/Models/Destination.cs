using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWTravelClubDB.Models
{
    public class Destination
    {
        [Key]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Country { get; set; }
        public string Description { get; set; }
        public ICollection<Package> Packages { get; set; }
    }
}
