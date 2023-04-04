namespace ClinicaVeterinaria.API.Api.model
{
    public class Vaccine
    {
        public Vaccine() { }
        public Vaccine(Guid petId, string name, DateOnly date)
        {
            Id = Guid.NewGuid();
            PetId = petId;
            Name = name;
            Date = date;
        }
        public Guid Id { get; set; }
        public Guid PetId { get; set; }
        [GraphQLNonNullType]
        public string Name { get; set; }
        [GraphQLNonNullType]
        public DateOnly Date { get; set; }
    }
}