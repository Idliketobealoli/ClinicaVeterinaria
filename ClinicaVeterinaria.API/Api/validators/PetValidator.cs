using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using System.Net.Mail;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class PetValidator
    {
        public static DomainError? Validate(this PetDTOcreate dto)
        {
            if (dto == null) return new PetErrorBadRequest("Data must not be null.");

            else if (!dto.Name.Trim().Any())
                return new PetErrorBadRequest("Name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                return new PetErrorBadRequest("Name must not be a single letter.");

            else if (!dto.Species.Trim().Any())
                return new PetErrorBadRequest("Species must not be null or blank.");

            else if (dto.Species.Trim().Length < 3)
                return new PetErrorBadRequest("Species must not be less than 3 letters.");

            else if (!dto.Race.Trim().Any())
                return new PetErrorBadRequest("Race must not be null or blank.");

            else if (dto.Race.Trim().Length < 3)
                return new PetErrorBadRequest("Race must not be less than 3 letters.");

            else if (dto.Weight <= 0)
                return new PetErrorBadRequest("Weight must not be equal to or lower than 0.");

            else if (dto.Size <= 0)
                return new PetErrorBadRequest("Size must not be equal to or lower than 0.");

            else if (dto.Date > DateOnly.FromDateTime(DateTime.Now))
                return new PetErrorBadRequest("Birth date must not be in the future.");

            else if (!MailAddress.TryCreate(dto.OwnerEmail.Trim(), out _))
                return new PetErrorBadRequest("Incorrect owner email address expression.");

            else return null;
        }

        public static DomainError? Validate(this PetDTOupdate dto)
        {
            if (dto == null) return new PetErrorBadRequest("Data must not be null.");

            else if (dto.Name != null && dto.Name.Trim().Length < 2)
                return new PetErrorBadRequest("Name must not be a single letter.");

            else if (dto.Weight != null && dto.Weight <= 0)
                return new PetErrorBadRequest("Weight must not be equal to or lower than 0.");

            else if (dto.Size != null && dto.Size <= 0)
                return new PetErrorBadRequest("Size must not be equal to or lower than 0.");

            else return null;
        }
    }
}
