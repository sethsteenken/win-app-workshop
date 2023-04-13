﻿using System.Text.Json.Serialization;

namespace Humongous.HealthCheck.API
{
    public class HealthCheck
    {
        [JsonPropertyName("id")]
        public string id { get; set; } //This property has to be lowercase "id" because of a bug in CosmosDB
        [JsonPropertyName("patientid")]
        public int PatientID { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("healthstatus")]
        public string HealthStatus { get; set; }
        [JsonPropertyName("symptoms")]
        public string[] Symptoms { get; set; }
    }
}
