using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;

namespace ClinicaVeterinaria.API.Api.controllers
{
    public class PetController
    {
        private readonly PetService Service;

        public PetController(PetService service)
        {
            Service = service;
        }

        public List<PetDTOshort> FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return task.Result;
        }

        public PetDTO FindById(Guid id)
        {
            var task = Service.FindById(id);
            task.Wait();
            return task.Result;
        }

        public PetDTOnoPhoto FindByIdNoPhoto(Guid id)
        {
            var task = Service.FindByIdNoPhoto(id);
            task.Wait();
            return task.Result;
        }

        public PetDTO Create(PetDTOcreate dto)
        {
            dto.Validate();

            var task = Service.Create(dto);
            task.Wait();
            return task.Result;
        }

        public PetDTO Update(PetDTOupdate dto)
        {
            dto.Validate();

            var task = Service.Update(dto);
            task.Wait();
            return task.Result;
        }

        public PetDTO Delete(Guid id)
        {
            var task = Service.Delete(id);
            task.Wait();
            return task.Result;
        }
    }
}
