using GrpcMicroServiceStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcMicroServiceStore
{
    public interface IMessageQueue
    {
        public Task<IList<QueueItem>> Top(int n);
        public Task Dequeue(IEnumerable<QueueItem> items);
        public Task Enqueue(QueueItem item);

    }
}
