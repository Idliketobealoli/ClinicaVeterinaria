using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.services;

namespace ClinicaVeterinaria.API.Api.controllers
{
    public class AppointmentController
    {
        private readonly AppointmentService Service;

        public AppointmentController(AppointmentService service)
        {
            Service = service;
        }

        public List<AppointmentDTOshort> FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return task.Result;
        }

        public AppointmentDTO FindById(Guid id)
        {
            var task = Service.FindById(id);
            task.Wait();
            return task.Result;
        }

        public AppointmentDTO Create(Appointment appointment)
        {
            var task = Service.Create(appointment);
            task.Wait();
            return task.Result;
        }

        public AppointmentDTO Delete(Guid id)
        {
            var task = Service.Delete(id);
            task.Wait();
            return task.Result;
        }
    }
}
