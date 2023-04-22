﻿using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ClinicaVeterinaria.API.Api.validators
{
    public static class VetValidator
    {
        public static DomainError? Validate(this VetDTOregister dto)
        {
            if (dto == null) return new VetErrorUnauthorized("Data must not be null.");

            else if (!dto.Name.Trim().Any())
                return new VetErrorUnauthorized("Name must not be null or blank.");

            else if (dto.Name.Trim().Length < 2)
                return new VetErrorUnauthorized("Name must not be a single letter.");

            else if (!dto.Surname.Trim().Any())
                return new VetErrorUnauthorized("Surname must not be null or blank.");

            else if (dto.Surname.Trim().Length < 2)
                return new VetErrorUnauthorized("Surname must not be a single letter.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                return new VetErrorUnauthorized("Incorrect email address expression.");

            else if (Regex.IsMatch(dto.SSNumber.Trim(),
                @"^(?!0{3})(?!6{3})[0-8]\d{2}-(?!0{2})\d{2}-(?!0{4})\d{4}$")
                ) // Un SSN valido seria 123-45-6789
                return new VetErrorUnauthorized("Incorrect social security number.");

            else if (dto.Password.Trim() != dto.RepeatPassword.Trim())
                return new VetErrorUnauthorized("Passwords do not match.");

            else if (!dto.Specialty.Trim().Any())
                return new VetErrorUnauthorized("Specialty must not be null or blank.");

            else return null;
        }

        public static DomainError? Validate(this VetDTOloginOrChangePassword dto)
        {
            if (dto == null) return new VetErrorUnauthorized("Data must not be null.");

            else if (!MailAddress.TryCreate(dto.Email.Trim(), out _))
                return new VetErrorUnauthorized("Incorrect email address expression.");

            else if (dto.Password.Length < 7)
                return new VetErrorBadRequest("Password must be at least 7 characters long.");

            else return null;
        }
    }
}
