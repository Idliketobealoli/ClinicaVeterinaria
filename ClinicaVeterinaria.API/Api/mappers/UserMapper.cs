using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.mappers
{
    internal static class UserMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            return new
                (
                user.Name,
                user.Surname,
                user.Email,
                user.Phone
                );
        }

        public static UserDTOshort ToDTOshort(this User user)
        {
            return new
                (
                user.Name,
                user.Surname
                );
        }
    }
}
