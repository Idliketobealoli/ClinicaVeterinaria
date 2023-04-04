namespace ClinicaVeterinaria.API.Api.model
{
    public class History
    {
        public History
            (
            Guid petId
            )
        {
            Id = Guid.NewGuid();
            PetId = petId;
            Vaccines = new HashSet<Vaccine>();
            AilmentTreatment = new Dictionary<string,string>();
        }

        public Guid Id { get; set; }
        public Guid PetId { get; set; }
        [GraphQLNonNullType]
        public HashSet<Vaccine> Vaccines { get; set; }
        [GraphQLNonNullType]
        public Dictionary<string, string> AilmentTreatment { get; set; }
    }
}