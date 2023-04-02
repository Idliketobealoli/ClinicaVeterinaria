namespace ClinicaVeterinaria.API.Api.model;

public class History
{
    public History(HashSet<Vaccine> vaccines, Dictionary<string, string> ailmentTreatment)
    {
        PetId = Guid.NewGuid();
        Vaccines = vaccines;
        this.ailmentTreatment = ailmentTreatment;
    }

    public Guid PetId { get; set; }
    public HashSet<Vaccine> Vaccines { get; set; }
    public Dictionary<string, string> ailmentTreatment { get; set; }
}