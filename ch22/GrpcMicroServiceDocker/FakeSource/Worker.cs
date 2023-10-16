using Grpc.Net.Client;
using GrpcMicroService;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace FakeSource;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string[] locations = new string[] { "Florence", "London", "New York", "Paris" };
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Random random = new Random();
        while (!stoppingToken.IsCancellationRequested)
        {
            
            try
            {
                var purchaseDay = new DateTimeOffset(DateTime.UtcNow.Date, TimeSpan.Zero);
                //randomize a little bit purchase day
                purchaseDay = purchaseDay.AddDays(random.Next(0, 3) - 1);  
                //message time
                var now = DateTimeOffset.UtcNow;
                //add random location
                var location = locations[random.Next(0, locations.Length)];
                var messageId = Guid.NewGuid().ToString();
                //add random cost
                int cost = 200 * random.Next(1, 4);
                //send message
                using var channel = GrpcChannel.ForAddress("http://grpcmicroservice:8080");
                var client = new Counter.CounterClient(channel);
                //since this is a fake random source
                //in case of errors we simply do nothing.
                //An actual client should use Polly
                //to define retry policies
                try
                {
                    await client.CountAsync(new CountingRequest
                    {
                        Id = messageId,
                        Location = location,
                        PurchaseTime = Timestamp.FromDateTimeOffset(purchaseDay),
                        Time = Timestamp.FromDateTimeOffset(now),
                        Cost = cost
                    });
                    
                }
                catch (Exception ex)
                {
                    var l = ex;
                }
                    
                
                await Task.Delay(2000, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch { }
        }
    }
}
