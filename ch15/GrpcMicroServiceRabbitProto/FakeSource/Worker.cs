using Polly;
using RabbitMQ.Client;
using Binaron;
using Binaron.Serializer;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO;

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
        var factory = new ConnectionFactory() { HostName = "localhost" };
        IConnection? connection =null;
        IModel? channel = null;
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    var purchaseDay = DateTime.UtcNow.Date;
                    //randomize a little bit purchase day
                    purchaseDay = purchaseDay.AddDays(random.Next(0, 3) - 1);

                    var purchase = new PurchaseMessage
                    {
                        //message time
                        PurchaseTime = Timestamp.FromDateTime(purchaseDay),
                        Time = Timestamp.FromDateTime(DateTime.UtcNow),
                        Id = Guid.NewGuid().ToString(),
                        //add random location
                        Location = locations[random.Next(0, locations.Length)],
                        //add random cost
                        Cost = 200 * random.Next(1, 4)
                    };
                    byte[]? body = null;
                    using (var stream = new MemoryStream())
                    {
                        purchase.WriteTo(stream);
                        stream.Flush();
                        body = stream.ToArray();
                    }
                    var policy = Policy
                        .Handle<Exception>()
                        .WaitAndRetry(6,
                            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                            retryAttempt)));
                    policy.Execute(() =>
                    {
                        try
                        {
                            if(connection == null || channel == null)
                            {
                                connection = factory.CreateConnection();
                                channel = connection.CreateModel();
                                channel.ConfirmSelect();
                            }

                            channel.QueueDeclare(queue: "purchase_queue",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
                            var properties = channel.CreateBasicProperties();
                            properties.Persistent = true;
                            channel.BasicPublish(exchange: "",
                                 routingKey: "purchase_queue",
                                 basicProperties: properties,
                                 body: body);

                            channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));

                        }
                        catch
                        {
                            channel.Dispose();
                            connection.Dispose();
                            channel = null;
                            connection = null;
                            throw;
                        }
                        
                     });



                    await Task.Delay(2000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                
            }
        }
        finally
        {
            if (connection != null)
            {
                channel.Dispose();
                connection.Dispose();
                channel = null;
                connection = null;
            }
        }
    }
}
