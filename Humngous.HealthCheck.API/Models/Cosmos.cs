using Microsoft.Azure.Cosmos;

namespace Humongous.HealthCheck.API
{
    internal static class Cosmos
    {
        public static CosmosClient CreateClient()
        {
            return new CosmosClient(Environment.GetEnvironmentVariable("CosmosConnectionString"));
        }

        public static async Task<IEnumerable<HealthCheck>> ReadToListAsync(this FeedIterator<HealthCheck> query)
        {
            var results = new List<HealthCheck>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public static Container GetHealthCheckContainer(this CosmosClient client)
        {
            return client.GetContainer(Environment.GetEnvironmentVariable("CosmosDatabase"), Environment.GetEnvironmentVariable("CosmosContainer"));
        }
    }
}
