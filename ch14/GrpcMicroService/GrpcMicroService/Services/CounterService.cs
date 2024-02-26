using Grpc.Core;
using GrpcMicroServiceStore;
using System;
using System.Threading.Tasks;

namespace GrpcMicroService.Services;
public class CounterService: Counter.CounterBase
{
    private readonly IMessageQueue queue;
    public CounterService(IMessageQueue queue)
    {
        if(queue == null) throw new ArgumentNullException(nameof(queue));
        this.queue = queue;
    }
    public override async  Task<CountingReply> Count(CountingRequest request, 
        ServerCallContext context)
    {

            await queue.Enqueue(new GrpcMicroServiceStore.Models.QueueItem
            {
                Cost = request.Cost,
                MessageId = Guid.Parse(request.Id),
                Location = request.Location,
                PurchaseTime = request.PurchaseTime.ToDateTimeOffset(),
                Time = request.Time.ToDateTimeOffset()
            });
            return new CountingReply {  };  
    }
}
