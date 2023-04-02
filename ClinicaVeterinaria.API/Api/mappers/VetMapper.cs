using ClinicaVeterinaria.API.Api.dto;
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
    }
}
