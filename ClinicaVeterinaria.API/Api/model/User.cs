namespace ClinicaVeterinaria.API.Api.model
{
    public class User
    {
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

        [GraphQLNonNullType]
        public Guid Id { get; set; }
        [GraphQLNonNullType]
        public string Name { get; set; }
        [GraphQLNonNullType]
        public string Surname { get; set; }
        [GraphQLNonNullType]
        public string Email { get; set; }
        [GraphQLNonNullType]
        public string Phone { get; set; }
        [GraphQLNonNullType]
        public string Password { get; set; }
    }
}
