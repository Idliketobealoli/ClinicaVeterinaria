using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.dto
{
    public class AppointmentDTO
    {
        public UserDTOshort User { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinishDate { get; set; }
        public PetDTOnoPhoto Pet { get; set; }
        public string Issue { get; set; }
        public State State { get; set; }
        public VetDTOappointment Vet { get; set; }

        public AppointmentDTO
            (
            UserDTOshort user,
            DateTime initial,
            DateTime finish,
            PetDTOnoPhoto pet,
            string issue,
            State state,
            VetDTOappointment vet
            )
        {
            User = user;
            InitialDate = initial;
            FinishDate = finish;
            Pet = pet;
            Issue = issue;
            State = state;
            Vet = vet;
        }
    }

    public class AppointmentDTOshort
    {
        public Guid Id { get; set; }
        public DateTime InitialDate { get; set; }
        public PetDTOnoPhoto Pet { get; set; }

        public AppointmentDTOshort
            (
            Guid id,
            DateTime initial,
            PetDTOnoPhoto pet
            )
        {
            Id = id;
            InitialDate = initial;
            Pet = pet;
        }
    }
}