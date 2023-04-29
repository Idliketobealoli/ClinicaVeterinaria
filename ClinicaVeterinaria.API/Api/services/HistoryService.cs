using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using ClinicaVeterinaria.API.Api.mappers;
using ClinicaVeterinaria.API.Api.repositories;

namespace ClinicaVeterinaria.API.Api.services
{
    public class HistoryService
    {
        private readonly HistoryRepository HisRepo;
        private readonly VaccineRepository VacRepo;

        public HistoryService(HistoryRepository hisRepo, VaccineRepository vacRepo)
        {
            HisRepo = hisRepo;
            VacRepo = vacRepo;
        }

        public virtual async Task<List<HistoryDTO>> FindAll()
        {
            var entities = await HisRepo.FindAll();
            var entitiesDTOs = new List<HistoryDTO>();
            foreach (var entity in entities)
            {
                entitiesDTOs.Add(entity.ToDTO());
            }
            return entitiesDTOs;
        }

        public virtual async Task<Either<HistoryDTO, DomainError>> FindByPetId(Guid id)
        {
            var entity = await HisRepo.FindByPetId(id);
            if (entity == null)
            {
                return new Either<HistoryDTO, DomainError>
                    (new HistoryErrorNotFound($"History with PetId {id} not found."));
            }
            else return new Either<HistoryDTO, DomainError>(entity.ToDTO());
        }

        public virtual async Task<Either<HistoryDTOvaccines, DomainError>> FindByPetIdVaccinesOnly(Guid id)
        {
            var entity = await HisRepo.FindByPetId(id);
            if (entity == null)
            {
                return new Either<HistoryDTOvaccines, DomainError>
                    (new HistoryErrorNotFound($"History with PetId {id} not found."));
            }
            else return new Either<HistoryDTOvaccines, DomainError>(entity.ToDTOvaccines());
        }

        public virtual async Task<Either<HistoryDTOailmentTreatment, DomainError>> FindByPetIdAilmTreatOnly(Guid id)
        {
            var entity = await HisRepo.FindByPetId(id);
            if (entity == null)
            {
                return new Either<HistoryDTOailmentTreatment, DomainError>
                    (new HistoryErrorNotFound($"History with PetId {id} not found."));
            }
            else return new Either<HistoryDTOailmentTreatment, DomainError>(entity.ToDTOailmentTreatment());
        }

        public virtual async Task<Either<HistoryDTO, DomainError>> AddVaccine(Guid id, VaccineDTO vaccine)
        {
            var history = await HisRepo.FindByPetId(id);
            if (history != null)
            {
                var newVaccine = vaccine.FromDTO(id);
                history.Vaccines.Add(newVaccine);
                await VacRepo.Create(newVaccine);
                await HisRepo.Update(history.Id, history);
                return new Either<HistoryDTO, DomainError>(history.ToDTO());
            }
            else return new Either<HistoryDTO, DomainError>
                    (new HistoryErrorNotFound($"History with PetId {id} not found."));
        }

        public virtual async Task<Either<HistoryDTO, DomainError>> AddAilmentTreatment(Guid id, string ailment, string treatment)
        {
            var history = await HisRepo.FindByPetId(id);
            if (history != null)
            {
                history.AilmentTreatment.TryAdd(ailment, treatment);
                await HisRepo.Update(history.Id, history);
                return new Either<HistoryDTO, DomainError>(history.ToDTO());
            }
            else return new Either<HistoryDTO, DomainError>
                    (new HistoryErrorNotFound($"History with PetId {id} not found."));
        }
    }
}
