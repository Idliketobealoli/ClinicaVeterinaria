﻿using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using ClinicaVeterinaria.API.Api.mappers;
using ClinicaVeterinaria.API.Api.repositories;

namespace ClinicaVeterinaria.API.Api.services
{
    public class PetService
    {
        private readonly PetRepository PetRepo;
        private readonly UserRepository UserRepo;
        private readonly HistoryRepository HisRepo;
        private readonly VaccineRepository VacRepo;

        public PetService(
            PetRepository petRepo, UserRepository userRepo,
            HistoryRepository hisRepo, VaccineRepository vacRepo
            )
        {
            PetRepo = petRepo;
            UserRepo = userRepo;
            HisRepo = hisRepo;
            VacRepo = vacRepo;
        }

        public PetService() { }

        public virtual async Task<List<PetDTOshort>> FindAll()
        {
            var pets = await PetRepo.FindAll();
            var petsDTO = new List<PetDTOshort>();
            foreach (var pet in pets)
            {
                petsDTO.Add(pet.ToDTOshort());
            }
            return petsDTO;
        }

        public virtual async Task<Either<PetDTO, DomainError>> FindById(Guid id)
        {
            var pet = await PetRepo.FindById(id);
            if (pet == null)
            {
                return new Either<PetDTO, DomainError>
                    (new PetErrorNotFound($"Pet with id {id} not found."));
            }
            var owner = await UserRepo.FindByEmail(pet.OwnerEmail);
            if (owner == null)
            {
                return new Either<PetDTO, DomainError>
                    (new UserErrorNotFound($"User with email {pet.OwnerEmail} not found."));
            }
            else return new Either<PetDTO, DomainError>(pet.ToDTO(owner));
        }

        public virtual async Task<Either<PetDTO, DomainError>> Create(PetDTOcreate dto)
        {
            var user = await UserRepo.FindByEmail(dto.OwnerEmail);
            if (user != null)
            {
                var pet = dto.FromDTO();
                var created = await PetRepo.Create(pet);
                await HisRepo.Create(pet.History);
                if (created != null)
                {
                    return new Either<PetDTO, DomainError>(created.ToDTO(user));
                }
                else return new Either<PetDTO, DomainError>
                        (new PetErrorBadRequest("Could not create pet."));
            }
            else return new Either<PetDTO, DomainError>
                    (new UserErrorNotFound($"Owner with email {dto.OwnerEmail} not found."));
        }

        public virtual async Task<Either<PetDTO, DomainError>> Update(PetDTOupdate dto)
        {
            var updated = await PetRepo.Update(dto);
            if (updated != null)
            {
                var owner = await UserRepo.FindByEmail(updated.OwnerEmail);
                if (owner != null)
                {
                    return new Either<PetDTO, DomainError>(updated.ToDTO(owner));
                }
                else return new Either<PetDTO, DomainError>
                        (new UserErrorNotFound($"User with email {updated.OwnerEmail} not found."));
            }
            else return new Either<PetDTO, DomainError>
                    (new PetErrorNotFound($"Pet with id {dto.Id} not found."));
        }

        public virtual async Task<Either<PetDTO, DomainError>> Delete(Guid id)
        {
            // esto primero porque si no despues no se encontrara
            // historial ni vacunas
            var pet = await PetRepo.FindById(id);
            if (pet == null)
            {
                return new Either<PetDTO, DomainError>
                    (new PetErrorNotFound($"Pet with id {id} not found."));
            }
            var owner = await UserRepo.FindByEmail(pet.OwnerEmail);
            if (owner == null)
            {
                return new Either<PetDTO, DomainError>
                    (new UserErrorNotFound($"User with email {pet.OwnerEmail} not found."));
            }
            var successfullResult = pet.ToDTO(owner);

            // borramos todas las vacunas de la mascota
            var vaccines = await VacRepo.FindAll();
            var filteredVaccines =
                from vaccine in vaccines
                where vaccine.PetId == id
                select vaccine;
            foreach ( var item in filteredVaccines )
            {
                await VacRepo.Delete(item.Id);
            }

            // borramos el historial de la mascota
            var history = await HisRepo.FindByPetId(id);
            if (history != null)
            {
                await HisRepo.Delete(history.Id);
            }

            // por ultimo, borramos la mascota y devolvemos el successful result
            var deleted = await PetRepo.Delete(id);
            if (deleted != null)
            {
                return new Either<PetDTO, DomainError>(successfullResult);
            }
            else return new Either<PetDTO, DomainError>
                    (new PetErrorBadRequest($"Could not delete Pet with id {id}."));
        }
    }
}
