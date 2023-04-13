namespace Humongous.HealthCheck.API
{
    internal class NewHealthCheck
    {
        public int patientid { get; set; }
        public string healthstatus { get; set; }
        public string[] symptoms { get; set; }
    }
}
