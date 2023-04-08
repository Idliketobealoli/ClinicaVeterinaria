namespace ClinicaVeterinaria.API.Api.exceptions
{
    public class PetException : Exception
    {
        public int Code { get; set; }

        public PetException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }

    public class PetNotFoundException : PetException
    {
        public PetNotFoundException(string message)
            : base(404, message) { }
    }

    public class PetBadRequestException : PetException
    {
        public PetBadRequestException(string message)
            : base(400, message) { }
    }
}
