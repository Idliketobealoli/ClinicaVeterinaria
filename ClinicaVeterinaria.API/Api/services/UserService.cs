using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using ClinicaVeterinaria.API.Api.mappers;
using ClinicaVeterinaria.API.Api.repositories;

namespace ClinicaVeterinaria.API.Api.services
{
    public class UserService
    {
        private readonly UserRepository Repo;

        public UserService(UserRepository repo)
        {
            Repo = repo;
        }

        public async Task<List<UserDTO>> FindAll()
        {
            var entities = await Repo.FindAll();
            var entitiesDTOs = new List<UserDTO>();
            foreach (var entity in entities)
            {
                entitiesDTOs.Add(entity.ToDTO());
            }
            return entitiesDTOs;
        }

        public async Task<Either<UserDTO, DomainError>> FindByEmail(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null)
            {
                return new Either<UserDTO, DomainError>
                    (new UserErrorNotFound($"User with Email {email} not found."));
            }
            else return new Either<UserDTO, DomainError>(user.ToDTO());
        }

        public async Task<Either<UserDTOshort, DomainError>> FindByEmailShort(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null)
            {
                return new Either<UserDTOshort, DomainError>
                    (new UserErrorNotFound($"User with Email {email} not found."));
            }
            else return new Either<UserDTOshort, DomainError>(user.ToDTOshort());
        }

        public async Task<Either<UserDTOandToken, DomainError>> Register(UserDTOregister dto)
        {
            var userByEmail = Repo.FindByEmail(dto.Email);
            var userByPhone = Repo.FindByPhone(dto.Phone);
            Task.WaitAll(userByEmail, userByPhone);
            if (userByPhone != null || userByEmail != null)
            {
                return new Either<UserDTOandToken, DomainError>
                    (new UserErrorUnauthorized("Cannot use either that email or that phone number."));
            }
            else
            {
                var user = dto.FromDTOregister();
                var created = await Repo.Create(user);
                if (created != null)
                {
                    return new Either<UserDTOandToken, DomainError>(created.toDTOwithToken());
                }
                else return new Either<UserDTOandToken, DomainError>
                        (new UserErrorUnauthorized("Could not register user."));
            }
        }

        public async Task<Either<UserDTOandToken, DomainError>> Login(UserDTOloginOrChangePassword dto)
        {
            var userByEmail = await Repo.FindByEmail(dto.Email);
            if (userByEmail == null || userByEmail.Password != dto.Password)
            {
                return new Either<UserDTOandToken, DomainError>
                    (new UserErrorUnauthorized("Incorrect email or password."));
            }
            else
            {
                return new Either<UserDTOandToken, DomainError>(userByEmail.toDTOwithToken());
            }
        }

        public async Task<Either<UserDTO, DomainError>> ChangePassword(UserDTOloginOrChangePassword dto)
        {
            var user = await Repo.UpdatePassword(dto.Email, dto.Password);
            if (user != null)
            {
                return new Either<UserDTO, DomainError>(user.ToDTO());
            }
            else return new Either<UserDTO, DomainError>
                    (new UserErrorNotFound($"User with email {dto.Email} not found."));
        }

        public async Task<Either<UserDTO, DomainError>> Delete(string email)
        {
            var user = await Repo.Delete(email);
            if (user != null)
            {
                return new Either<UserDTO, DomainError>(user.ToDTO());
            }
            else return new Either<UserDTO, DomainError>
                    (new UserErrorNotFound($"User with email {email} not found."));
        }
    }
}
