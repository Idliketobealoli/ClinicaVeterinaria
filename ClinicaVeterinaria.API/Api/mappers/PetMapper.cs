using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.repositories;

namespace ClinicaVeterinaria.API.Api.mappers
{
    internal static class PetMapper
    {
        public static PetDTOshort ToDTOshort(this Pet pet)
        {
            return new
                (
                pet.Id,
                pet.Photo,
                pet.Name,
                pet.Race,
                pet.Species,
                pet.Sex
                );
        }

        public static async Task<PetDTO> ToDTO(this Pet pet, UserRepository repo)
        {
            var owner = await repo.FindByEmail(pet.OwnerEmail) ??
                throw new UserNotFoundException($"User with email {pet.OwnerEmail} not found.");
            return new
                (
                pet.Id,
                pet.Photo,
                pet.Name,
                pet.Race,
                pet.Species,
                pet.Sex,
                pet.BirthDate,
                pet.Weight,
                pet.Size,
                pet.History.ToDTO(),
                owner.ToDTOshort()
                );
        }

        public static PetDTOnoPhoto ToDTOnoPhoto(this Pet pet)
        {
            return new
                (
                pet.Name,
                pet.Race,
                pet.Species,
                pet.Sex
                );
        }

        public static Pet FromDTO(this PetDTOcreate dto)
        {
            return new
                (
                Guid.NewGuid(),
                dto.Name,
                dto.Species,
                dto.Race,
                dto.Weight,
                dto.Size,
                dto.Sex,
                dto.Date,
                dto.OwnerEmail,
                dto.Photo
                );
        }
    }
}
