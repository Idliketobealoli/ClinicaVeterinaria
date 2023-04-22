using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using System.Net.Mail;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class UserValidator
    {
        public static DomainError? Validate(this UserDTOregister dto)
        {
            if (dto == null) return new UserErrorUnauthorized("Data must not be null.");

            else if (!dto.Name.Trim().Any())
                return new UserErrorUnauthorized("Name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                return new UserErrorUnauthorized("Name must not be a single letter.");

            else if (!dto.Surname.Trim().Any())
                return new UserErrorUnauthorized("Surname must not be null or blank.");

            else if (dto.Surname.Trim().Length < 2)
                return new UserErrorUnauthorized("Surname must not be a single letter.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                return new UserErrorUnauthorized("Incorrect email address expression.");

            else if (dto.Phone.Trim().Length < 9)
                return new UserErrorUnauthorized("Phone number too short to be correct.");

            else if (dto.Password.Trim() != dto.RepeatPassword.Trim())
                return new UserErrorUnauthorized("Passwords do not match.");
            else return null;
        }

        public static DomainError? Validate(this UserDTOloginOrChangePassword dto)
        {
            if (dto == null) return new UserErrorUnauthorized("Data must not be null.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                return new UserErrorUnauthorized("Incorrect email address expression.");

            else if (dto.Password.Length < 7)
                return new UserErrorBadRequest("Password must be at least 7 characters long.");
            else return null;
        }
    }
}