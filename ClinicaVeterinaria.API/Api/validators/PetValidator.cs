using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
using System.Net.Mail;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class PetValidator
    {
        public static bool Validate(this PetDTOcreate dto)
        {
            if (dto == null) throw new PetBadRequestException("Data must not be null.");

            else if (!dto.Name.Trim().Any())
                throw new PetBadRequestException("Name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                throw new PetBadRequestException("Name must not be a single letter.");

            else if (!dto.Species.Trim().Any())
                throw new PetBadRequestException("Species must not be null or blank.");

            else if (dto.Species.Trim().Length < 3)
                throw new PetBadRequestException("Species must not be less than 3 letters.");

            else if (!dto.Race.Trim().Any())
                throw new PetBadRequestException("Race must not be null or blank.");

            else if (dto.Race.Trim().Length < 3)
                throw new PetBadRequestException("Race must not be less than 3 letters.");

            else if (dto.Weight <= 0)
                throw new PetBadRequestException("Weight must not be equal to or lower than 0.");

            else if (dto.Size <= 0)
                throw new PetBadRequestException("Size must not be equal to or lower than 0.");

            else if (dto.Date > DateOnly.FromDateTime(DateTime.Now))
                throw new PetBadRequestException("Birth date must not be in the future.");

            else if (!MailAddress.TryCreate(dto.OwnerEmail.Trim(), out _))
                throw new PetBadRequestException("Incorrect owner email address expression.");

            else return true;
        }

        public static bool Validate(this PetDTOupdate dto)
        {
            if (dto == null) throw new PetBadRequestException("Data must not be null.");

            else if (dto.Name != null && dto.Name.Trim().Length < 2)
                throw new PetBadRequestException("Name must not be a single letter.");

            else if (dto.Weight != null && dto.Weight <= 0)
                throw new PetBadRequestException("Weight must not be equal to or lower than 0.");

            else if (dto.Size != null && dto.Size <= 0)
                throw new PetBadRequestException("Size must not be equal to or lower than 0.");

            else return true;
        }
    }
}
