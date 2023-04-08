namespace ClinicaVeterinaria.API.Api.exceptions
{
    public class AppointmentException : Exception
    {
        public int Code { get; set; }

        public AppointmentException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }

    public class AppointmentNotFoundException : AppointmentException
    {
        public AppointmentNotFoundException(string message)
            : base(404, message) { }
    }

    public class AppointmentBadRequestException : AppointmentException
    {
        public AppointmentBadRequestException(string message)
            : base(400, message) { }
    }
}
