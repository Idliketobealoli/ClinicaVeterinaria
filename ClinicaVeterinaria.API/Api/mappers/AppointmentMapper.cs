using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.services;

namespace ClinicaVeterinaria.API.Api.mappers
{
    internal static class AppointmentMapper
    {
        public static AppointmentDTO ToDTO
            (
            this Appointment appointment, UserService uService,
            PetService pService, VetService vService
            )
        {
            var user = uService.FindByEmailShort(appointment.UserEmail);
            var pet = pService.FindByIdNoPhoto(appointment.PetId);
            var vet = vService.FindByEmailAppointment(appointment.VetEmail);
            Task.WaitAll(user, pet, vet);

            return new
                (
                user.Result ?? throw new UserNotFoundException($"User with email {appointment.UserEmail} not found."),
                appointment.InitialDate,
                appointment.FinishDate,
                pet.Result ?? throw new Exception(),
                appointment.Issue,
                appointment.State,
                vet.Result ?? throw new VetNotFoundException($"Vet with email {appointment.VetEmail} not found.")
                );
        }

        public static async Task<AppointmentDTOshort> ToDTOshort
            (
            this Appointment appointment, PetService pService
            )
        {
            var pet = await pService.FindByIdNoPhoto(appointment.PetId);

            return new
                (
                appointment.Id,
                appointment.InitialDate,
                pet
                );
        }
    }
}
