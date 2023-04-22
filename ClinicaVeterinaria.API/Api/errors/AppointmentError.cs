namespace ClinicaVeterinaria.API.Api.errors
{
    public class AppointmentError : DomainError
    {
        public AppointmentError(int code, string message)
            : base(code, message) { }
    }

    public class AppointmentErrorNotFound : AppointmentError
    {
        public AppointmentErrorNotFound(string message)
            : base(404, message) { }
    }

    public class AppointmentErrorBadRequest : AppointmentError
    {
        public AppointmentErrorBadRequest(string message)
            : base(400, message) { }
    }
}
