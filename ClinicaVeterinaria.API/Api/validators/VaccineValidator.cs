using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class VaccineValidator
    {
        public static DomainError? Validate(this VaccineDTO dto)
        {
            if (dto == null) return new HistoryError(400, "Vaccine is null.");

            else if (!dto.Name.Trim().Any())
                return new HistoryError(400, "Vaccine name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                return new HistoryError(400, "Vaccine name must not be a single letter.");

            else return null;
        }
    }
}
