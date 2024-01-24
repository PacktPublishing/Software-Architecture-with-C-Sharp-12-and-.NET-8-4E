using GrpcMicroServiceStore;
using GrpcMicroServiceStore.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Binaron.Serializer;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

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
        
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "purchase_queueb",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (sender, ea) =>
                    {
                        if (stoppingToken.IsCancellationRequested)
                        {
                            channel.Close();
                            connection.Close();
                            return;
                        }
                        using (var scope = services.CreateScope())
                        {
                            try
                            {
                                IDayStatistics statistics = scope.ServiceProvider.GetRequiredService<IDayStatistics>();
                                var body = ea.Body.ToArray();
                                PurchaseMessage? message = null;
                                using (var stream = new MemoryStream(body))
                                {
                                    message = BinaronConvert.Deserialize<PurchaseMessage>(stream);
                                }
                                var res = await statistics.Add(new Purchase { 
                                    Cost= message.Cost,
                                    Id=message.Id,
                                    Location = message.Location,
                                    Time = new DateTimeOffset(message.Time, TimeSpan.Zero),
                                    PurchaseTime = new DateTimeOffset(message.PurchaseTime, TimeSpan.Zero)
                                });


                                // Note: it is possible to access the channel via
                                //       ((EventingBasicConsumer)sender).Model here
                                if(res == null)
                                    ((EventingBasicConsumer)sender).Model.BasicNack(ea.DeliveryTag, false, true);
                                else
                                    ((EventingBasicConsumer)sender).Model.BasicAck(ea.DeliveryTag, false);
                            }
                            catch { }
                        }
                    };
                   
                    channel.BasicConsume(queue: "purchase_queueb",
                                autoAck: false,
                                consumer: consumer);
                    await Task.Delay(1000, stoppingToken);
                }
             }
            catch { }
        }
    }
}
