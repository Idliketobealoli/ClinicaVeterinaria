using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
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

        public async Task<List<PetDTOshort>> FindAll()
        {
            var pets = await PetRepo.FindAll();
            var petsDTO = new List<PetDTOshort>();
            foreach (var pet in pets)
            {
                petsDTO.Add(pet.ToDTOshort());
            }
            return petsDTO;
        }

        public async Task<PetDTO> FindById(Guid id)
        {
            var pet = await PetRepo.FindById(id);
            if (pet == null) { throw new PetNotFoundException($"Pet with id {id} not found."); }
            else return await pet.ToDTO(UserRepo);
        }

        public async Task<PetDTOnoPhoto> FindByIdNoPhoto(Guid id)
        {
            var pet = await PetRepo.FindById(id);
            if (pet == null) { throw new PetNotFoundException($"Pet with id {id} not found."); }
            else return pet.ToDTOnoPhoto();
        }

        public async Task<PetDTO> Create(PetDTOcreate dto)
        {
            var user = await UserRepo.FindByEmail(dto.OwnerEmail);
            if (user != null)
            {
                var pet = dto.FromDTO();
                var created = await PetRepo.Create(pet);
                await HisRepo.Create(pet.History);
                if (created != null)
                {
                    return await created.ToDTO(UserRepo);
                }
                else throw new PetBadRequestException("Could not create pet.");
            }
            else throw new UserNotFoundException($"Owner with email {dto.OwnerEmail} not found.");
        }

        public async Task<PetDTO> Update(PetDTOupdate dto)
        {
            var updated = await PetRepo.Update(dto);
            if (updated != null)
            {
                return await updated.ToDTO(UserRepo);
            }
            else throw new PetNotFoundException($"Pet with id {dto.Id} not found.");
        }

        public async Task<PetDTO> Delete(Guid id)
        {
            // esto primero porque si no despues no se encontrara
            // historial ni vacunas
            var pet = await PetRepo.FindById(id)
                ?? throw new PetNotFoundException($"Pet with id {id} not found.");
            var successfullResult = pet.ToDTO(UserRepo);

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
                return await successfullResult;
            }
            else throw new PetBadRequestException($"Could not delete Pet with id {id}.");
        }
    }
}
