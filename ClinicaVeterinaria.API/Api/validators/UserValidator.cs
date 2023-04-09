using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
using System.Net.Mail;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class UserValidator
    {
        public static void Validate(this UserDTOregister dto)
        {
            if (dto == null) throw new UserUnauthorizedException("Data must not be null.");

            else if (!dto.Name.Trim().Any())
                throw new UserUnauthorizedException("Name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                throw new UserUnauthorizedException("Name must not be a single letter.");

            else if (!dto.Surname.Trim().Any())
                throw new UserUnauthorizedException("Surname must not be null or blank.");

            else if (dto.Surname.Trim().Length < 2)
                throw new UserUnauthorizedException("Surname must not be a single letter.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                throw new UserUnauthorizedException("Incorrect email address expression.");

            else if (dto.Phone.Trim().Length < 9)
                throw new UserUnauthorizedException("Phone number too short to be correct.");

            else if (dto.Password.Trim() != dto.RepeatPassword.Trim())
                throw new UserUnauthorizedException("Passwords do not match.");
        }

        public static void Validate(this UserDTOloginOrChangePassword dto)
        {
            if (dto == null) throw new UserUnauthorizedException("Data must not be null.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                throw new UserUnauthorizedException("Incorrect email address expression.");

            else if (dto.Password.Length < 7)
                throw new UserBadRequestException("Password must be at least 7 characters long.");
        }
    }
}