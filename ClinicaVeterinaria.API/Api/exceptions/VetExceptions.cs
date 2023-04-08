namespace ClinicaVeterinaria.API.Api.exceptions
{
    public class VetException : Exception
    {
        public int Code { get; set; }
        public VetException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }

    public class VetNotFoundException : UserException
    {
        public VetNotFoundException(string message)
            : base(404, message) { }
    }

    public class VetBadRequestException : UserException
    {
        public VetBadRequestException(string message)
            : base(400, message) { }
    }

    public class VetUnauthorizedException : UserException
    {
        public VetUnauthorizedException(string message)
            : base(401, message) { }
    }

    public class VetForbiddenException : UserException
    {
        public VetForbiddenException(string message)
            : base(403, message) { }
    }
}
