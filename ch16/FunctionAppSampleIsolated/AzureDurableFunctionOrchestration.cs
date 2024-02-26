using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace FunctionAppSampleIsolated
{
    public static class AzureDurableFunctionOrchestration
    {
        [Function(nameof(HelloCities))]
        public static async Task<string> HelloCities([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            string result = "";
            result += await context.CallActivityAsync<string>(nameof(SayHello), "Tokyo") + " ";
            result += await context.CallActivityAsync<string>(nameof(SayHello), "London") + " ";
            result += await context.CallActivityAsync<string>(nameof(SayHello), "Seattle");
            return result;
        }

        [Function(nameof(SayHello))]
        public static string SayHello([ActivityTrigger] string cityName, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(SayHello));
            logger.LogInformation($"Saying hello to {cityName}.");
            Thread.Sleep(5000); 
            return $"Hello {cityName}!";
        }

        [Function(nameof(StartHelloCities))]
        public static async Task<HttpResponseData> StartHelloCities(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(StartHelloCities));

            // Function input comes from the request content.
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(HelloCities));
            logger.LogInformation("Created new orchestration with instance ID = {instanceId}", instanceId);

            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}