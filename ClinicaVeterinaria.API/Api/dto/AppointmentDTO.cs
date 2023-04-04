using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.dto
{
    public class AppointmentDTO
    {
        public UserDTOshort User { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinishDate { get; set; }
        public PetNoPhotoDTO Pet { get; set; }
        public string Issue { get; set; }
        public State State { get; set; }
        public VetDTOshort Vet { get; set; }

        public AppointmentDTO
            (
            UserDTOshort user,
            DateTime initial,
            DateTime finish,
            PetNoPhotoDTO pet,
            string issue,
            State state,
            VetDTOshort vet
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
        public DateTime InitialDate { get; set; }
        public PetNoPhotoDTO Pet { get; set; }

        public AppointmentDTOshort
            (
            DateTime initial,
            PetNoPhotoDTO pet
            )
        {
            InitialDate = initial;
            Pet = pet;
        }
    }
}