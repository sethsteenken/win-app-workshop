using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Humongous.HealthCheck.API
{
    public class SaveSubmission
    {
        [Function("SaveSubmission")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "healthcheck/submissions")] HttpRequestData req)
        {
            using var client = Cosmos.CreateClient();

            var data = await req.ReadFromJsonAsync<NewHealthCheck>();

            var healthCheck = new HealthCheck()
            {
                id = Guid.NewGuid().ToString(),
                Date = DateTime.UtcNow,
                PatientID = data.patientid,
                HealthStatus = data.healthstatus,
                Symptoms = data.symptoms
            };

            var createResponse = await client.GetHealthCheckContainer()
                                             .CreateItemAsync(healthCheck, new PartitionKey(healthCheck.id));

            if (createResponse == null)
                throw new InvalidOperationException("Item failed to save to database.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json");

            await response.WriteAsJsonAsync<HealthCheck>(healthCheck);

            return response;
        }
    }
}
