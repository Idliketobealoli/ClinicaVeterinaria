﻿using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.dto
{
    public class VetDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string SSNumber { get; set; }
        public Role Role { get; set; }
        public string Specialty { get; set; }

        public VetDTO
            (
            string name,
            string surname,
            string email,
            string ssnum,
            Role role,
            string specialty
            )
        {
            Name = name;
            Surname = surname;
            Email = email;
            SSNumber = ssnum;
            Role = role;
            Specialty = specialty;
        }
    }

    public class VetDTOappointment
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public VetDTOappointment
            (
            string name,
            string surname,
            string email
            )
        {
            Name = name;
            Surname = surname;
            Email = email;
        }
    }

    public class VetDTOloginOrChangePassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public VetDTOloginOrChangePassword
            (
            string email,
            string password
            )
        {
            Email = email;
            Password = password;
        }
    }

    public class VetDTOregister
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string SSNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public Role Role { get; set; }
        public string Specialty { get; set; }

        public VetDTOregister()
        {
            Name = "";
            Surname = "";
            Email = "";
            SSNumber = "";
            Password = "";
            RepeatPassword = "";
            Role = Role.VET;
            Specialty = "";
        }

        public VetDTOregister
            (
            string name,
            string surname,
            string email,
            string ssnum,
            string password,
            string repeatPassword,
            Role role,
            string specialty
            )
        {
            Name = name;
            Surname = surname;
            Email = email;
            SSNumber = ssnum;
            Password = password;
            RepeatPassword = repeatPassword;
            Role = role;
            Specialty = specialty;
        }
    }

    public class VetDTOshort
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public VetDTOshort
            (
            string name,
            string surname
            )
        {
            Name = name;
            Surname = surname;
        }
    }

    public class VetDTOandToken
    {
        public VetDTO DTO { get; set; }
        public string Token { get; set; }

        public VetDTOandToken(VetDTO dTO, string token)
        {
            DTO = dTO;
            Token = token;
        }
    }
}
