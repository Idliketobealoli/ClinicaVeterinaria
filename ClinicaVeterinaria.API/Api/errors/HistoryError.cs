namespace ClinicaVeterinaria.API.Api.errors
{
    public class HistoryError : DomainError
    {

        public HistoryError(int code, string message)
            : base(code, message) { }
    }

    public class HistoryErrorNotFound : HistoryError
    {
        public HistoryErrorNotFound(string message)
            : base(404, message) { }
    }
}
