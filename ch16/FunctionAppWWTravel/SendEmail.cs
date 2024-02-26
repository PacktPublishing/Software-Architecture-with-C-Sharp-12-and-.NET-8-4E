using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FunctionAppWWTravel
{
    public class SendEmail
    {

        private readonly ILogger _logger;

        public SendEmail(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SendEmail>();
        }


    private static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

        [Function(nameof(SendEmail))]
        public async Task<HttpResponseMessage> RunAsync(
                [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            bool error = false;
            var requestData = await req.ReadAsStringAsync();

            var connectionString = Environment.GetEnvironmentVariable("EmailQueueConnectionString");

            var queueClient = new QueueClient(connectionString, "email", new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64 // or QueueMessageEncoding.None
            });

            if (!string.IsNullOrEmpty(requestData))
            {
                var result = await queueClient.SendMessageAsync(requestData);

                if ((HttpStatusCode)result.GetRawResponse().Status != HttpStatusCode.Created)
                    error = true;
            }
            else
                error = true;

            if (error)
            {
                _logger.LogError("Error sending message to queue");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { success = false }), Encoding.UTF8, "application/json"),
                };
            }
            else
            {
                _logger.LogInformation("HTTP trigger from SendEmail function processed a request.");
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { success = true }), Encoding.UTF8, "application/json"),
                };

            }

        }
    }
    
}
