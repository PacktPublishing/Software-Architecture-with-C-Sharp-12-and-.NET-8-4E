using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrpcMicroServiceStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcMicroServiceStore
{
    internal class DayStatistics : IDayStatistics
    {
        private readonly MainDbContext ctx;
        public DayStatistics(IUnitOfWork uw)
        {
            ctx = (MainDbContext)uw;
        }
        public async Task<QueueItem?> Add(QueueItem model)
        {
            using var transaction = ctx.Database.BeginTransaction();
            try
            {
                bool processed = await ctx.Purchases.AnyAsync(m => m.Id == model.MessageId);
                if (processed) return model;
                ctx.Purchases.Add(new Purchase
                {
                    Cost=model.Cost,
                    Id=model.MessageId,
                    Location=model.Location,
                    PurchaseTime=model.PurchaseTime,
                    Time = model.Time
                    
                });
                var slot = await ctx.DayTotals.Where(m => m.Id == model.PurchaseTime)
                    .FirstOrDefaultAsync();
                if(slot == null)
                {
                    slot = new DayTotal
                    {
                        Id = model.PurchaseTime,
                        Total = 0
                    };
                    ctx.DayTotals.Add(slot);
                }
                slot.Total += model.Cost;
                await ctx.SaveChangesAsync();
                transaction.Commit();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public async Task<decimal> DayTotal(DateTimeOffset day)
        {
            return await ctx.DayTotals.Where(m => m.Id == day)
                .Select(m => m.Total)
                .FirstOrDefaultAsync();
        }
    }
}
