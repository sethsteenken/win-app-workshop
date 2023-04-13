using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Humongous.HealthCheck.API
{
    public class GetSubmission
    {
        [Function("GetSubmission")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "healthcheck/submissions/{id}/")] HttpRequestData req,
            string id)
        {
            using var client = Cosmos.CreateClient();

            var item = await client.GetHealthCheckContainer()
                                   .ReadItemAsync<HealthCheck>(id, new PartitionKey(id));

            if (item == null || item.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException("Item not found in database.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json");

            await response.WriteAsJsonAsync<HealthCheck>(item.Resource);

            return response;
        }
    }
}
