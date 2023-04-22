namespace ClinicaVeterinaria.API.Api.errors
{
    public class PetError : DomainError
    {
        public PetError(int code, string message)
            : base(code, message) { }
    }

    public class PetErrorNotFound : PetError
    {
        public PetErrorNotFound(string message)
            : base(404, message) { }
    }

    public class PetErrorBadRequest : PetError
    {
        public PetErrorBadRequest(string message)
            : base(400, message) { }
    }
}
