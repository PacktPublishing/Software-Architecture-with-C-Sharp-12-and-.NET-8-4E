using System;
using System.Collections.Generic;
using System.Text;

namespace WWTravelClubDBTest
{
    public class PackagesListDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public DateTime? StartValidityDate { get; set; }
        public DateTime? EndValidityDate { get; set; }
        public string DestinationName { get; set; }
        public string DestinationId { get; set; }
        public override string ToString()
        {
            return string.Format("{0}. {1} days in {2}, price: {3}", 
                Name, DurationInDays, DestinationName, Price);
        }
    }
}
