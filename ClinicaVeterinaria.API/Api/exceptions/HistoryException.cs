namespace ClinicaVeterinaria.API.Api.exceptions
{
    public class HistoryException : Exception
    {
        public int Code { get; set; }

        public HistoryException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }

    public class HistoryNotFoundException : HistoryException
    {
        public HistoryNotFoundException(string message)
            : base(404, message) { }
    }
}
