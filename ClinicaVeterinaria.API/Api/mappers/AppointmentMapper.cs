using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.services;

namespace ClinicaVeterinaria.API.Api.mappers
{
    internal static class AppointmentMapper
    {
        public static async Task<AppointmentDTO> ToDTO
            (
            this Appointment appointment, UserService uService,
            PetService pService, VetService vService
            )
        {
            var user = await uService.FindByEmailShort(appointment.UserEmail);
            var pet = await pService.FindByIdNoPhoto(appointment.PetId);
            var vet = await vService.FindByEmailAppointment(appointment.VetEmail);

            return new
                (
                user,
                appointment.InitialDate,
                appointment.FinishDate,
                pet,
                appointment.Issue,
                appointment.State,
                vet
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
