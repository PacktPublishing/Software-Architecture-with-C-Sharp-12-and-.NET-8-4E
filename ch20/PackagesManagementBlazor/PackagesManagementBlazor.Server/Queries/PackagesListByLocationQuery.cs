using Microsoft.EntityFrameworkCore;
using PackagesManagementBlazor.Shared;
using PackagesManagementDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagementBlazor.Server.Queries
{
    public class PackagesListByLocationQuery:IPackagesListByLocationQuery
    {
        private readonly MainDbContext ctx;
        public PackagesListByLocationQuery(MainDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<IEnumerable<PackageInfosViewModel>> GetPackagesOf(string location)
        {
            return await ctx.Packages
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
                .ToListAsync();
        }
    }
}
