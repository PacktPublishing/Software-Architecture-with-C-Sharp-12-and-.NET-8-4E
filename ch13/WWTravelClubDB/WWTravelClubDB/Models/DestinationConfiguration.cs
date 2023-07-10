using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WWTravelClubDB.Models
{
    internal class DestinationConfiguration : 
        IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder
               .HasIndex(m => m.Country);

            builder
                .HasIndex(m => m.Name);
        }
    }
}
