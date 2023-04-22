namespace ClinicaVeterinaria.API.Api.errors
{
    public class UserError : DomainError
    {
        public UserError(int code, string message)
            : base(code, message) { }
    }

    public class UserErrorNotFound : UserError
    {
        public UserErrorNotFound(string message)
            : base(404, message) { }
    }

    public class UserErrorBadRequest : UserError
    {
        public UserErrorBadRequest(string message)
            : base(400, message) { }
    }

    public class UserErrorUnauthorized : UserError
    {
        public UserErrorUnauthorized(string message)
            : base(401, message) { }
    }

    public class UserErrorForbidden : UserError
    {
        public UserErrorForbidden(string message)
            : base(403, message) { }
    }
}
