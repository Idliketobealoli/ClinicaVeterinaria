using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.dto;

internal class HistoryFindAllDTO
{
    public HistoryFindAllDTO(HashSet<Vaccine> vaccines, Dictionary<string, string> ailmentTreatment)
    {
        Vaccines = vaccines;
        this.ailmentTreatment = ailmentTreatment;
    }

    public HashSet<Vaccine> Vaccines { get; set; }
    public Dictionary<string, string> ailmentTreatment { get; set; }
}

internal class HistoryVaccinesDTO
{
    public HistoryVaccinesDTO(HashSet<Vaccine> vaccines)
    {
        Vaccines = vaccines;
    }

    public HashSet<Vaccine> Vaccines { get; set; }
}

internal class HistoryAilmentTreatmentDTO
{
    public HistoryAilmentTreatmentDTO(Dictionary<string, string> ailmentTreatment)
    {
        this.ailmentTreatment = ailmentTreatment;
    }

    public Dictionary<string, string> ailmentTreatment { get; set; }
}