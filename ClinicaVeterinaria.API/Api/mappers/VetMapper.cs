﻿using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.mappers
{
    internal static class VetMapper
    {
        public static VetDTO ToDTO(this Vet vet)
        {
            return new
                (
                vet.Name,
                vet.Surname,
                vet.Email,
                vet.SSNumber,
                vet.Role,
                vet.Specialty
                );
        }

        public static VetDTOshort ToDTOshort(this Vet vet)
        {
            return new
                (
                vet.Name,
                vet.Surname
                );
        }

        public static VetDTOappointment ToDTOappointment(this Vet vet)
        {
            return new
                (
                vet.Name,
                vet.Surname,
                vet.Email
                );
        }

        public static Vet FromDTOregister(this VetDTOregister dto)
        {
            return new
                (
                dto.Name,
                dto.Surname,
                dto.Email,
                dto.SSNumber,
                dto.Password,
                dto.Role,
                dto.Specialty
                );
        }

        public static VetDTOandToken ToDTOwithToken(this Vet vet)
        {
            return new
                (
                vet.ToDTO(),
                "token" // esto habrá que modificarlo cuando metamos tokens
                );
        }
    }
}
