using ClinicaVeterinaria.API.Api.dto;
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

        public async Task<UserDTO> FindByEmail(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null) { throw new Exception(); }
            else return user.ToDTO();
        }

        public async Task<UserDTOshort> FindByEmailShort(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null) { throw new Exception(); }
            else return user.ToDTOshort();
        }

        public async Task<UserDTOandToken> Register(UserDTOregister dto)
        {
            // TODO: aqui primero hay que validar
            var userByEmail = Repo.FindByEmail(dto.Email);
            var userByPhone = Repo.FindByPhone(dto.Phone);
            Task.WaitAll(userByEmail, userByPhone);
            if (userByPhone != null || userByEmail != null)
            {
                throw new Exception();
            }
            else
            {
                var user = dto.FromDTOregister();
                var created = await Repo.Create(user);
                if (created != null)
                {
                    return created.toDTOwithToken();
                }
                else throw new Exception();
            }
        }

        public async Task<UserDTOandToken> Login(UserDTOloginOrChangePassword dto)
        {
            // TODO: aqui primero la validacion
            var userByEmail = await Repo.FindByEmail(dto.Email);
            if (userByEmail == null || userByEmail.Password != dto.Password)
            {
                throw new Exception();
            }
            else
            {
                return userByEmail.toDTOwithToken();
            }
        }

        public async Task<UserDTO> ChangePassword(UserDTOloginOrChangePassword dto)
        {
            var user = await Repo.UpdatePassword(dto.Email, dto.Password);
            if (user != null)
            {
                return user.ToDTO();
            }
            else throw new Exception();
        }

        public async Task<UserDTO> Delete(string email)
        {
            var user = await Repo.Delete(email);
            if (user != null)
            {
                return user.ToDTO();
            }
            else throw new Exception();
        }
    }
}
