using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.mappers
{
    internal static class PetMapper
    {
        public static PetFindAllDTO ToFindAllDTO(this Pet pet)
        {
            return new
                (
                pet.Photo,
                pet.Name,
                pet.Race,
                pet.Species,
                pet.Sex
                );
        }

        public static PetIdDTO ToFindOneDTO(this Pet pet)
        {
            return new
                (
                pet.Photo,
                pet.Name,
                pet.Race,
                pet.Species,
                pet.Sex,
                pet.BirthDate,
                pet.Weight,
                pet.Size,
                pet.History,
                // owner
                );
        }
    }
}
