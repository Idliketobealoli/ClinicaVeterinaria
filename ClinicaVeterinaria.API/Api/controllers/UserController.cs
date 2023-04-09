using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;

namespace ClinicaVeterinaria.API.Api.controllers
{
    public class UserController
    {
        private readonly UserService Service;

        public UserController(UserService service)
        {
            Service = service;
        }

        public List<UserDTO> FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return task.Result;
        }

        public UserDTO FindByEmail(string email)
        {
            var task = Service.FindByEmail(email);
            task.Wait();
            return task.Result;
        }

        public UserDTOshort FindByEmailShort(string email)
        {
            var task = Service.FindByEmailShort(email);
            task.Wait();
            return task.Result;
        }

        public UserDTOandToken Register(UserDTOregister dto)
        {
            dto.Validate();

            var task = Service.Register(dto);
            task.Wait();
            return task.Result;
        }

        public UserDTOandToken Login(UserDTOloginOrChangePassword dto)
        {
            dto.Validate();

            var task = Service.Login(dto);
            task.Wait();
            return task.Result;
        }

        public UserDTO ChangePassword(UserDTOloginOrChangePassword dto)
        {
            dto.Validate();

            var task = Service.ChangePassword(dto);
            task.Wait();
            return task.Result;
        }

        public UserDTO Delete(string email)
        {
            var task = Service.Delete(email);
            task.Wait();
            return task.Result;
        }
    }
}
