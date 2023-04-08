using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class VetValidator
    {
        public static bool Validate(this VetDTOregister dto)
        {
            if (dto == null) throw new VetUnauthorizedException("Data must not be null.");

            else if (!dto.Name.Trim().Any())
                throw new VetUnauthorizedException("Name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                throw new VetUnauthorizedException("Name must not be a single letter.");

            else if (!dto.Surname.Trim().Any())
                throw new VetUnauthorizedException("Surname must not be null or blank.");

            else if (dto.Surname.Trim().Length < 2)
                throw new VetUnauthorizedException("Surname must not be a single letter.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                throw new VetUnauthorizedException("Incorrect email address expression.");

            else if (Regex.IsMatch(dto.SSNumber.Trim(),
                @"^(?!0{3})(?!6{3})[0-8]\d{2}-(?!0{2})\d{2}-(?!0{4})\d{4}$")
                ) // Un SSN valido seria 123-45-6789
                throw new VetUnauthorizedException("Incorrect social security number.");

            else if (dto.Password.Trim() != dto.RepeatPassword.Trim())
                throw new VetUnauthorizedException("Passwords do not match.");

            else if (!dto.Specialty.Trim().Any())
                throw new VetUnauthorizedException("Specialty must not be null or blank.");

            else return true;
        }

        public static bool Validate(this VetDTOloginOrChangePassword dto)
        {
            if (dto == null) throw new VetUnauthorizedException("Data must not be null.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                throw new VetUnauthorizedException("Incorrect email address expression.");

            else if (dto.Password.Length < 7)
                throw new VetBadRequestException("Password must be at least 7 characters long.");

            else return true;
        }
    }
}
