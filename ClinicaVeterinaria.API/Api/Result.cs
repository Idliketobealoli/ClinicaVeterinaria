namespace ClinicaVeterinaria.API.Api
{
    public class Result
    {
        public int StatusCode { get; set; }
        public string Value { get; set; }
        public long ResponseTimeMillis { get; set; }

        public Result(int statusCode, string value, long responseTimeMillis)
        {
            StatusCode = statusCode;
            Value = value;
            ResponseTimeMillis = responseTimeMillis;
        }
    }
}
