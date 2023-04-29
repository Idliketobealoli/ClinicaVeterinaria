using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using ClinicaVeterinaria.API.Api.mappers;
using ClinicaVeterinaria.API.Api.repositories;

namespace ClinicaVeterinaria.API.Api.services
{
    public class VetService
    {
        private readonly VetRepository Repo;

        public VetService(VetRepository repo)
        {
            Repo = repo;
        }

        public VetService() { }

        public virtual async Task<List<VetDTO>> FindAll()
        {
            var entities = await Repo.FindAll();
            var entitiesDTOs = new List<VetDTO>();
            foreach (var entity in entities)
            {
                entitiesDTOs.Add(entity.ToDTO());
            }
            return entitiesDTOs;
        }

        public virtual async Task<Either<VetDTO, DomainError>> FindByEmail(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null)
            {
                return new Either<VetDTO, DomainError>
                    (new VetErrorNotFound($"Vet with email {email} not found."));
            }
            else return new Either<VetDTO, DomainError>(user.ToDTO());
        }

        public virtual async Task<Either<VetDTOshort, DomainError>> FindByEmailShort(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null)
            {
                return new Either<VetDTOshort, DomainError>
                    (new VetErrorNotFound($"Vet with email {email} not found."));
            }
            else return new Either<VetDTOshort, DomainError>(user.ToDTOshort());
        }

        public virtual async Task<Either<VetDTOappointment, DomainError>> FindByEmailAppointment(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null)
            {
                return new Either<VetDTOappointment, DomainError>
                    (new VetErrorNotFound($"Vet with email {email} not found."));
            }
            else return new Either<VetDTOappointment, DomainError>(user.ToDTOappointment());
        }

        public virtual async Task<Either<VetDTOandToken, DomainError>> Register(VetDTOregister dto)
        {
            var userByEmail = Repo.FindByEmail(dto.Email);
            var userBySSNumber = Repo.FindBySSNum(dto.SSNumber);
            Task.WaitAll(userByEmail, userBySSNumber);
            if (userBySSNumber.Result != null || userByEmail.Result != null)
            {
                return new Either<VetDTOandToken, DomainError>
                    (new VetErrorUnauthorized("Cannot use either that email or that Social Security number."));
            }
            else
            {
                var user = dto.FromDTOregister();
                var created = await Repo.Create(user);
                if (created != null)
                {
                    return new Either<VetDTOandToken, DomainError>(created.ToDTOwithToken());
                }
                else return new Either<VetDTOandToken, DomainError>
                    (new VetErrorUnauthorized("Could not register vet."));
            }
        }

        public virtual async Task<Either<VetDTOandToken, DomainError>> Login(VetDTOloginOrChangePassword dto)
        {
            var userByEmail = await Repo.FindByEmail(dto.Email);
            if (userByEmail == null || userByEmail.Password != dto.Password)
            {
                return new Either<VetDTOandToken, DomainError>
                    (new VetErrorUnauthorized("Incorrect email or password."));
            }
            else
            {
                return new Either<VetDTOandToken, DomainError>(userByEmail.ToDTOwithToken());
            }
        }

        public virtual async Task<Either<VetDTO, DomainError>> ChangePassword(VetDTOloginOrChangePassword dto)
        {
            var user = await Repo.UpdatePassword(dto.Email, dto.Password);
            if (user != null)
            {
                return new Either<VetDTO, DomainError>(user.ToDTO());
            }
            else return new Either<VetDTO, DomainError>
                    (new VetErrorNotFound($"Vet with email {dto.Email} not found."));
        }

        public virtual async Task<Either<VetDTO, DomainError>> Delete(string email)
        {
            var user = await Repo.Delete(email);
            if (user != null)
            {
                return new Either<VetDTO, DomainError>(user.ToDTO());
            }
            else return new Either<VetDTO, DomainError>
                    (new VetErrorNotFound($"Vet with email {email} not found."));
        }
    }
}
