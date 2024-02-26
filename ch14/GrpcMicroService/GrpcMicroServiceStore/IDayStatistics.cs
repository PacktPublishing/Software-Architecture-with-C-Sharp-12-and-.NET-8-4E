using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrpcMicroServiceStore.Models;

namespace GrpcMicroServiceStore
{
    public interface IDayStatistics
    {
        Task<decimal> DayTotal(DateTimeOffset day);
        Task<QueueItem?> Add(QueueItem model);
    }
}
