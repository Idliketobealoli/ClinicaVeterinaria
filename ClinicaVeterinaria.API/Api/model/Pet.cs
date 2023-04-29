namespace ClinicaVeterinaria.API.Api.model
{
    public class Pet
    {
        public Pet
            (
            Guid id, string name, string species, string race,
            double weight, double size, Sex sex, DateOnly birthDate,
            string ownerEmail
            )
        {
            Id = id;
            Name = name;
            Species = species;
            Race = race;
            Weight = weight;
            Size = size;
            Sex = sex;
            BirthDate = birthDate;
            OwnerEmail = ownerEmail;
            History = new History(id);
        }

        [GraphQLNonNullType]
        public Guid Id { get; set; }
        [GraphQLNonNullType]
        public string Name { get; set; }
        [GraphQLNonNullType]
        public string Species { get; set; }
        [GraphQLNonNullType]
        public string Race { get; set; }
        [GraphQLNonNullType]
        public double Weight { get; set; }
        [GraphQLNonNullType]
        public double Size { get; set; }
        [GraphQLNonNullType]
        public Sex Sex { get; set; }
        [GraphQLNonNullType]
        public DateOnly BirthDate { get; set; }
        [GraphQLNonNullType]
        public string OwnerEmail { get; set; }
        [GraphQLNonNullType]
        public History History { get; set; }
    }

    public enum Sex
    {
        MALE,
        FEMALE
    }
}