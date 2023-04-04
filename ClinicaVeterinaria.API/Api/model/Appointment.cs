namespace ClinicaVeterinaria.API.Api.model
{
    public class Appointment
    {
        public Appointment
            (
            string userEmail,
            DateTime initialDate,
            DateTime finishDate,
            Guid petId,
            string issue,
            string vetEmail
            )
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            InitialDate = initialDate;
            FinishDate = finishDate;
            PetId = petId;
            Issue = issue;
            State = State.PENDING;
            VetEmail = vetEmail;
        }

        [GraphQLNonNullType]
        public Guid Id { get; set; }
        [GraphQLNonNullType]
        public string UserEmail { get; set; }
        [GraphQLNonNullType]
        public DateTime InitialDate { get; set; }
        [GraphQLNonNullType]
        public DateTime FinishDate { get; set; }
        [GraphQLNonNullType]
        public Guid PetId { get; set; }
        [GraphQLNonNullType]
        public string Issue { get; set; }
        [GraphQLNonNullType]
        public State State { get; set; }
        [GraphQLNonNullType]
        public string VetEmail { get; set; }
    }

    public enum State
    {
        PENDING,
        PROGRESS,
        FINISHED
    }
}
