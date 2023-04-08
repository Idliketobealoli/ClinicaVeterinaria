namespace ClinicaVeterinaria.API.Api.exceptions
{
    public class UserException : Exception
    {
        public int Code { get; set; }
        public UserException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }

    public class UserNotFoundException : UserException
    {
        public UserNotFoundException(string message)
            : base(404, message) { }
    }

    public class UserBadRequestException : UserException
    {
        public UserBadRequestException(string message)
            : base(400, message) { }
    }

    public class UserUnauthorizedException : UserException
    {
        public UserUnauthorizedException(string message)
            : base(401, message) { }
    }

    public class UserForbiddenException : UserException
    {
        public UserForbiddenException(string message)
            : base(403, message) { }
    }
}
