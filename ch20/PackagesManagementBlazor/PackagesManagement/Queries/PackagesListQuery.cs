using Microsoft.EntityFrameworkCore;
using PackagesManagement.Models.Packages;
using PackagesManagementDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Queries
{
    public class PackagesListQuery:IPackagesListQuery
    {
        private readonly MainDbContext ctx;
        public PackagesListQuery(MainDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<IEnumerable<PackageInfosViewModel>> GetAllPackages()
        {
            return await ctx.Packages.Select(m => new PackageInfosViewModel
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
