using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;

namespace ClinicaVeterinaria.API.Api.controllers
{
    public class HistoryController
    {
        private readonly HistoryService Service;

        public HistoryController(HistoryService service)
        {
            Service = service;
        }

        public List<HistoryDTO> FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return task.Result;
        }

        public HistoryDTO FindByPetId(Guid id)
        {
            var task = Service.FindByPetId(id);
            task.Wait();
            return task.Result;
        }

        public HistoryDTOvaccines FindByPetIdVaccinesOnly(Guid id)
        {
            var task = Service.FindByPetIdVaccinesOnly(id);
            task.Wait();
            return task.Result;
        }

        public HistoryDTOailmentTreatment FindByPetIdAilmTreatOnly(Guid id)
        {
            var task = Service.FindByPetIdAilmTreatOnly(id);
            task.Wait();
            return task.Result;
        }

        public HistoryDTO AddVaccine(Guid id, VaccineDTO vaccine)
        {
            vaccine.Validate();

            var task = Service.AddVaccine(id, vaccine);
            task.Wait();
            return task.Result;
        }

        public HistoryDTO AddAilmentTreatment(Guid id, string ailment, string treatment)
        {
            var task = Service.AddAilmentTreatment(id, ailment, treatment);
            task.Wait();
            return task.Result;
        }
    }
}
