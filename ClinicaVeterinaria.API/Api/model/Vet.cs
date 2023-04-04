namespace ClinicaVeterinaria.API.Api.model
{
    public class Vet
    {
        public Vet
            (
            string name,
            string surname,
            string email,
            string sSNumber,
            string password,
            Role role,
            string specialty
            )
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            SSNumber = sSNumber;
            Password = password;
            Role = role;
            Specialty = specialty;
        }

        [GraphQLNonNullType]
        public Guid Id { get; set; }
        [GraphQLNonNullType]
        public string Name { get; set; }
        [GraphQLNonNullType]
        public string Surname { get; set; }
        [GraphQLNonNullType]
        public string Email { get; set; }
        [GraphQLNonNullType]
        public string SSNumber { get; set; }
        [GraphQLNonNullType]
        public string Password { get; set; }
        [GraphQLNonNullType]
        public Role Role { get; set; }
        [GraphQLNonNullType]
        public string Specialty { get; set; }
    }

    public enum Role
    {
        VET,
        ADMIN
    }
}
