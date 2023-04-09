using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;

namespace ClinicaVeterinaria.API.Api.controllers
{
    public class VetController
    {
        private readonly VetService Service;

        public VetController(VetService service)
        {
            Service = service;
        }

        public List<VetDTO> FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return task.Result;
        }

        public VetDTO FindByEmail(string email)
        {
            var task = Service.FindByEmail(email);
            task.Wait();
            return task.Result;
        }

        public VetDTOshort FindByEmailShort(string email)
        {
            var task = Service.FindByEmailShort(email);
            task.Wait();
            return task.Result;
        }

        public VetDTOappointment FindByEmailAppointment(string email)
        {
            var task = Service.FindByEmailAppointment(email);
            task.Wait();
            return task.Result;
        }

        public VetDTOandToken Register(VetDTOregister dto)
        {
            dto.Validate();

            var task = Service.Register(dto);
            task.Wait();
            return task.Result;
        }

        public VetDTOandToken Login(VetDTOloginOrChangePassword dto)
        {
            dto.Validate();

            var task = Service.Login(dto);
            task.Wait();
            return task.Result;
        }

        public VetDTO ChangePassword(VetDTOloginOrChangePassword dto)
        {
            dto.Validate();

            var task = Service.ChangePassword(dto);
            task.Wait();
            return task.Result;
        }

        public VetDTO Delete(string email)
        {
            var task = Service.Delete(email);
            task.Wait();
            return task.Result;
        }
    }
}
