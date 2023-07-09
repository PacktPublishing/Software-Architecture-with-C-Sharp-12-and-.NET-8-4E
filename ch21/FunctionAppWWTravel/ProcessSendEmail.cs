using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppWWTravel
{
    public static class ProcessSendEmail
    {
        [FunctionName(nameof(ProcessSendEmail))]
        public static void Run([QueueTrigger("email", Connection = "QueueConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
