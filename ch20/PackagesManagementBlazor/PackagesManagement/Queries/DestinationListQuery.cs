using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PackagesManagementDB;

namespace PackagesManagement.Queries
{
    public class DestinationListQuery : IDestinationListQuery
    {
        private readonly MainDbContext ctx;
        public DestinationListQuery(MainDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<IEnumerable<SelectListItem>> AllDestinations()
        {
            return (await ctx.Destinations.Select(m => new
            {
                Text = m.Country,
                Value = m.Id
            })
            .OrderBy(m => m.Text)
            .ToListAsync())
            .Select(m => new SelectListItem
            {
                Text = m.Text,
                Value = m.Value.ToString()
            });
        }
    }
}
