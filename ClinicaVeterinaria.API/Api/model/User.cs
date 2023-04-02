namespace ClinicaVeterinaria.API.Api.model
{
    internal class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public User
            (
            string name,
            string surname,
            string email,
            string phone,
            string password
            )
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Password = password;
        }
    }
}
