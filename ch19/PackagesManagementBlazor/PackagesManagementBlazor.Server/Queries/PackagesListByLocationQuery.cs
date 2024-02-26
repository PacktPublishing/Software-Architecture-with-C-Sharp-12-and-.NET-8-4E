using Microsoft.EntityFrameworkCore;
using PackagesManagementBlazor.Shared;
using PackagesManagementDB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagementBlazor.Server.Queries
{
    public class PackagesListByLocationQuery(MainDbContext ctx) : IPackagesListByLocationQuery
    {
        
        public async Task<ReadOnlyCollection<PackageInfosViewModel>> GetPackagesOfAsync(string location)
        {
            return new ReadOnlyCollection<PackageInfosViewModel>( await ctx.Packages
                .Where(m => m.MyDestination.Name.StartsWith(location))
                .Select(m => new PackageInfosViewModel
            {
                StartValidityDate = m.StartValidityDate,
                EndValidityDate = m.EndValidityDate,
                Name = m.Name,
                DurationInDays = m.DurationInDays,
                Id = m.Id,
                Price = m.Price,
                DestinationName = m.MyDestination.Name,
                DestinationId = m.DestinationId
            })
                .OrderByDescending(m=> m.EndValidityDate)
                .ToListAsync());
        }
    }
}
