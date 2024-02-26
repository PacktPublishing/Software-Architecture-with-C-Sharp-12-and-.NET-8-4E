using GrpcMicroServiceStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcMicroServiceStore
{
    internal class MessageQueue : IMessageQueue
    {
        private readonly MainDbContext ctx;
        private const double MinuteBlocked = 3;
        public MessageQueue(IUnitOfWork uw)
        {
            ctx = (MainDbContext)uw;
        }
        public async Task Dequeue(IEnumerable<QueueItem> items)
        {
            foreach (var item in items) 
            {
                ctx.QueueItems.Remove(item);
            }
            await ctx.SaveChangesAsync();
        }

        public  async Task Enqueue(QueueItem item)
        {
            ctx.QueueItems.Add(item);
            await ctx.SaveChangesAsync();
        }

        public async Task<IList<QueueItem>> Top(int n)
        {
            using var transaction = ctx.Database.BeginTransaction();
            try
            {
                var limit = DateTimeOffset.Now.AddMinutes(-MinuteBlocked);
                var res = ctx.QueueItems
                    .OrderByDescending(m => m.Time)
                    .Where(m => m.ExtractionTime < limit)
                    .ToList();
                if(res.Count>0)
                {
                    var now = DateTimeOffset.Now;
                    foreach (var item in res)
                    {
                        item.ExtractionTime = now;
                    }
                    await ctx.SaveChangesAsync();
                    transaction.Commit();
                }
                return res;
            }
            catch
            {
                return new List<QueueItem>();
            }
        }
    }
}
