namespace ClinicaVeterinaria.API.Api.errors
{
    public class VetError : DomainError
    {
        public VetError(int code, string message)
            : base(code, message) { }
    }

    public class VetErrorNotFound : VetError
    {
        public VetErrorNotFound(string message)
            : base(404, message) { }
    }

    public class VetErrorBadRequest : VetError
    {
        public VetErrorBadRequest(string message)
            : base(400, message) { }
    }

    public class VetErrorUnauthorized : VetError
    {
        public VetErrorUnauthorized(string message)
            : base(401, message) { }
    }

    public class VetErrorForbidden : VetError
    {
        public VetErrorForbidden(string message)
            : base(403, message) { }
    }
}
