using ClinicaVeterinaria.API.Api.dto;
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

        public async Task<List<HistoryDTO>> FindAll()
        {
            var entities = await HisRepo.FindAll();
            var entitiesDTOs = new List<HistoryDTO>();
            foreach (var entity in entities)
            {
                entitiesDTOs.Add(entity.ToDTO());
            }
            return entitiesDTOs;
        }

        public async Task<HistoryDTO?> FindByPetId(Guid id)
        {
            var entity = await HisRepo.FindByPetId(id);
            if (entity == null) { return null; }
            else return entity.ToDTO();
        }

        public async Task<HistoryDTOvaccines?> FindByPetIdVaccinesOnly(Guid id)
        {
            var entity = await HisRepo.FindByPetId(id);
            if (entity == null) { return null; }
            else return entity.ToDTOvaccines();
        }

        public async Task<HistoryDTOailmentTreatment?> FindByPetIdAilmTreatOnly(Guid id)
        {
            var entity = await HisRepo.FindByPetId(id);
            if (entity == null) { return null; }
            else return entity.ToDTOailmentTreatment();
        }

        public async Task<HistoryDTO> AddVaccine(Guid id, VaccineDTO vaccine)
        {
            var history = await HisRepo.FindByPetId(id);
            if (history != null)
            {
                var newVaccine = vaccine.FromDTO(id);
                history.Vaccines.Add(newVaccine);
                await VacRepo.Create(newVaccine);
                await HisRepo.Update(history.Id, history);
                return history.ToDTO();
            }
            else throw new Exception();
        }

        public async Task<HistoryDTO> AddAilmentTreatment(Guid id, string ailment, string treatment)
        {
            var history = await HisRepo.FindByPetId(id);
            if (history != null)
            {
                history.AilmentTreatment.TryAdd(ailment, treatment);
                await HisRepo.Update(history.Id, history);
                return history.ToDTO();
            }
            else throw new Exception();
        }
    }
}
