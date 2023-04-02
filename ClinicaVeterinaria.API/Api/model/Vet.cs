namespace ClinicaVeterinaria.API.Api.model
{
    internal class Vet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string SSNumber { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Specialty { get; set; }

        public Vet
            (
            string name,
            string surname,
            string email,
            string ssnum,
            string password,
            Role role,
            string specialty
            )
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            SSNumber = ssnum;
            Password = password;
            Role = role;
            Specialty = specialty;
        }
    }

    enum Role
    {
        VET,
        ADMIN
    }
}
