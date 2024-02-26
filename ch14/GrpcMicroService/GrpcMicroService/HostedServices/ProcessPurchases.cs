using GrpcMicroServiceStore;
using GrpcMicroServiceStore.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcMicroService.HostedServices;
public class ProcessPurchases : BackgroundService
{
    IServiceProvider services;
    public ProcessPurchases(IServiceProvider services)
    {
        this.services = services;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        bool queueEmpty = false;
        while (!stoppingToken.IsCancellationRequested)
        {
            while (!queueEmpty && !stoppingToken.IsCancellationRequested)
            {
                using (var scope = services.CreateScope())
                {
                    IMessageQueue queue = scope.ServiceProvider.GetRequiredService<IMessageQueue>();
                    
                    var toProcess = await queue.Top(10);
                    if (toProcess.Count > 0)
                    {
                        Task<QueueItem?>[] tasks = new Task<QueueItem?>[toProcess.Count];
                        for (int i = 0; i < tasks.Length; i++)
                        {
                            var toExecute = async () =>
                            {
                                QueueItem? item;
                                using (var sc = services.CreateScope())
                                {
                                    IDayStatistics statistics = sc.ServiceProvider.GetRequiredService<IDayStatistics>();
                                    item = await statistics.Add(toProcess[i]);
                                }
                                return item;
                            };
                            tasks[i] = toExecute();
                        }
                        await Task.WhenAll(tasks);
                        await queue.Dequeue(tasks.Select(m => m.Result).Where(m => m != null).OfType<QueueItem>());
                    }
                    else queueEmpty = true;
                }
            }   
            await Task.Delay(100, stoppingToken);
            queueEmpty = false;
        }
    }
}
