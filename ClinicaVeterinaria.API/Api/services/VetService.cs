using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.exceptions;
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

        public async Task<List<VetDTO>> FindAll()
        {
            var entities = await Repo.FindAll();
            var entitiesDTOs = new List<VetDTO>();
            foreach (var entity in entities)
            {
                entitiesDTOs.Add(entity.ToDTO());
            }
            return entitiesDTOs;
        }

        public async Task<VetDTO> FindByEmail(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null) { throw new VetNotFoundException($"Vet with email {email} not found."); }
            else return user.ToDTO();
        }

        public async Task<VetDTOshort> FindByEmailShort(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null) { throw new VetNotFoundException($"Vet with email {email} not found."); }
            else return user.ToDTOshort();
        }

        public async Task<VetDTOappointment> FindByEmailAppointment(string email)
        {
            var user = await Repo.FindByEmail(email);
            if (user == null) { throw new VetNotFoundException($"Vet with email {email} not found."); }
            else return user.ToDTOappointment();
        }

        public async Task<VetDTOandToken> Register(VetDTOregister dto)
        {
            var userByEmail = Repo.FindByEmail(dto.Email);
            var userBySSNumber = Repo.FindBySSNum(dto.SSNumber);
            Task.WaitAll(userByEmail, userBySSNumber);
            if (userBySSNumber != null || userByEmail != null)
            {
                throw new VetUnauthorizedException("Cannot use either that email or that Social Security number.");
            }
            else
            {
                var user = dto.FromDTOregister();
                var created = await Repo.Create(user);
                if (created != null)
                {
                    return created.ToDTOwithToken();
                }
                else throw new VetUnauthorizedException("Could not register vet.");
            }
        }

        public async Task<VetDTOandToken> Login(VetDTOloginOrChangePassword dto)
        {
            // TODO: aqui primero la validacion
            var userByEmail = await Repo.FindByEmail(dto.Email);
            if (userByEmail == null || userByEmail.Password != dto.Password)
            {
                throw new VetUnauthorizedException("Incorrect email or password.");
            }
            else
            {
                return userByEmail.ToDTOwithToken();
            }
        }

        public async Task<VetDTO> ChangePassword(VetDTOloginOrChangePassword dto)
        {
            var user = await Repo.UpdatePassword(dto.Email, dto.Password);
            if (user != null)
            {
                return user.ToDTO();
            }
            else throw new VetNotFoundException($"Vet with email {dto.Email} not found.");
        }

        public async Task<VetDTO> Delete(string email)
        {
            var user = await Repo.Delete(email);
            if (user != null)
            {
                return user.ToDTO();
            }
            else throw new VetNotFoundException($"Vet with email {email} not found.");
        }
    }
}
