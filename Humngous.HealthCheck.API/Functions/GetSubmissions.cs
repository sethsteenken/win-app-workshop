using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Humongous.HealthCheck.API
{
    public class GetSubmissions
    {
        [Function("GetSubmissions")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "healthcheck/submissions")] HttpRequestData req)
        {
            using var client = Cosmos.CreateClient();

            var submissions = await client.GetHealthCheckContainer()
                                          .GetItemQueryIterator<HealthCheck>("SELECT * FROM c")
                                          .ReadToListAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json");

            await response.WriteAsJsonAsync<IEnumerable<HealthCheck>>(submissions);

            return response;
        }
    }
}
