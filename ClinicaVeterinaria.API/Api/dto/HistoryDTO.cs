using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.dto
{
    public class HistoryFindAllDTO
    {
        public HistoryFindAllDTO(HashSet<Vaccine> vaccines, Dictionary<string, string> ailmentTreatment)
        {
            Vaccines = vaccines;
            AilmentTreatment = ailmentTreatment;
        }

        public HashSet<Vaccine> Vaccines { get; set; }
        public Dictionary<string, string> AilmentTreatment { get; set; }
    }

    public class HistoryVaccinesDTO
    {
        public HistoryVaccinesDTO(HashSet<Vaccine> vaccines)
        {
            Vaccines = vaccines;
        }

        public HashSet<Vaccine> Vaccines { get; set; }
    }

    public class HistoryAilmentTreatmentDTO
    {
        public HistoryAilmentTreatmentDTO(Dictionary<string, string> ailmentTreatment)
        {
            AilmentTreatment = ailmentTreatment;
        }

        public Dictionary<string, string> AilmentTreatment { get; set; }
    }
}