namespace ClinicaVeterinaria.API.Api.model
{
    internal class Appointment
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinishDate { get; set; }
        public Guid PetId { get; set; }
        public string Issue { get; set; }
        public State State { get; set; }
        public string VetEmail { get; set; }

        public Appointment
            (
            string userEmail,
            DateTime initial,
            DateTime finish,
            Guid petId,
            string issue,
            string vetEmail
            )
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            InitialDate = initial;
            FinishDate = finish;
            PetId = petId;
            Issue = issue;
            State = State.PENDING;
            VetEmail = vetEmail;
        }
    }

    enum State
    {
        PENDING,
        PROGRESS,
        FINISHED
    }
}
