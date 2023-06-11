using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CosmosDBClientSample
{
    class Program
    {
        private const string endpoint = "insert here your account URL";
        private const string key = "insert here your key";

        private const string databaseId = "WWTravelClub";
        private const string containerId = "admin";

        public static async Task Main()
        {
            try
            {
                await CreateCosmosDB();
            }
            catch (CosmosException de)
            {
                Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        public static async Task CreateCosmosDB()
        {
            using var cosmosClient = new CosmosClient(endpoint, key);
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            ContainerProperties cp = new ContainerProperties(containerId, "/DestinationName");
            Container container = await database.CreateContainerIfNotExistsAsync(cp);
            await AddItemsToContainerAsync(container);
        }


        private static async Task AddItemsToContainerAsync(Container container)
        {
            var destinationToAdd = new Destination
            {
                Id = "1",
                Country = "Brazil",
                DestinationName = "São Paulo",
                Description = "The biggest city in Brazil",
                Packages = new[]
                {
                    new Package
                    {
                        Id = "1",
                        Description = "Visit Paulista Avenue",
                        DurationInDays = 3,
                        Price = 5000,
                    }
                }
            };


            try
            {
                await container.ReadItemAsync<Destination>(destinationToAdd.Id, new PartitionKey(destinationToAdd.DestinationName));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                await container.CreateItemAsync<Destination>(destinationToAdd, new PartitionKey(destinationToAdd.DestinationName));
            }
        }
    }

    public class Destination
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string DestinationName { get; set; }

        public string Country { get; set; }
        public string Description { get; set; }
        public Package[] Packages { get; set; }
    }

    public class Package
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public DateTime? StartValidityDate { get; set; }
        public DateTime? EndValidityDate { get; set; }
        public Destination MyDestination { get; set; }
        public string DestinationId { get; set; }
    }
}
