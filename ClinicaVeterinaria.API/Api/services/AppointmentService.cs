using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
using ClinicaVeterinaria.API.Api.mappers;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.repositories;

namespace ClinicaVeterinaria.API.Api.services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository Repo;
        private readonly PetService PetService;
        private readonly UserService UserService;
        private readonly VetService VetService;

        public AppointmentService
            (
            AppointmentRepository repo, PetService petService,
            UserService userService, VetService vetService
            )
        {
            Repo = repo;
            PetService = petService;
            UserService = userService;
            VetService = vetService;
        }

        public async Task<List<AppointmentDTOshort>> FindAll()
        {
            var entities = await Repo.FindAll();
            var entitiesDTOs = new List<AppointmentDTOshort>();
            foreach (var entity in entities)
            {
                entitiesDTOs.Add(await entity.ToDTOshort(PetService));
            }
            return entitiesDTOs;
        }

        public async Task<AppointmentDTO> FindById(Guid id)
        {
            var task = await Repo.FindById(id);
            if (task == null) { throw new AppointmentNotFoundException($"Appointment with id {id} not found."); }
            else return task.ToDTO(UserService, PetService, VetService);
        }

        public async Task<AppointmentDTO> Create(Appointment appointment)
        {
            var userByEmail = UserService.FindByEmail(appointment.UserEmail);
            var vetByEmail = VetService.FindByEmail(appointment.VetEmail);
            var allAppointments = Repo.FindAll();
            var pet = PetService.FindByIdNoPhoto(appointment.PetId);

            Task.WaitAll(userByEmail, vetByEmail, allAppointments, pet);

            IEnumerable<Appointment>? newList = new List<Appointment>();
            if (allAppointments != null)
            {
                newList =
                from ap in await allAppointments
                where (ap.InitialDate == appointment.InitialDate)
                select ap;
            }
            if (
                userByEmail != null &&      // Si el usuario existe en la DB.
                vetByEmail != null &&       // Si el veterinario existe en la DB.
                !newList.Any() &&           // Si no hay otras citas en esa hora.
                appointment.InitialDate     // Si la fecha de inicio es
                < appointment.FinishDate && //  anterior a la de fin.
                pet != null                 // Si la mascota existe en la DB.
                )
            {
                var created = await Repo.Create(appointment);
                if (created != null)
                {
                    return created.ToDTO(UserService, PetService, VetService);
                }
                else throw new AppointmentBadRequestException("Could not create appointment.");
            }
            else throw new AppointmentBadRequestException("Incorrect data for the new appointment.");
        }

        public async Task<AppointmentDTO> Delete(Guid id)
        {
            var appointment = await Repo.FindById(id);
            if (appointment == null) { throw new AppointmentNotFoundException($"Appointment with id {id} not found."); }
            var successfulResult = appointment.ToDTO(UserService, PetService, VetService);

            var deleted = await Repo.Delete(id);
            if (deleted != null)
            {
                return successfulResult;
            }
            else throw new AppointmentBadRequestException($"Could not delete Appointment with id {id}.");
        }
    }
}
