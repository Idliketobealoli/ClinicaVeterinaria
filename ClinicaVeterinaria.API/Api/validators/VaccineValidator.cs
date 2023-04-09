using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class VaccineValidator
    {
        public static bool Validate(this VaccineDTO dto)
        {
            if (dto == null) throw new HistoryException(400, "Vaccine is null.");

            else if (!dto.Name.Trim().Any())
                throw new HistoryException(400, "Vaccine name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                throw new HistoryException(400, "Vaccine name must not be a single letter.");

            else return true;
        }
    }
}
