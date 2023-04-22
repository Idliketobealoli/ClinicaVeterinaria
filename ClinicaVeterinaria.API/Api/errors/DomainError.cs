namespace ClinicaVeterinaria.API.Api.errors
{
    public abstract class DomainError
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public DomainError(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
