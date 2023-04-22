using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.schema;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;
using System.Text.Json;

namespace ClinicaVeterinaria.API.Api.controllers
{
    [ExtendObjectType(typeof(Query))]
    public class VetController
    {
        private readonly VetService Service;

        public VetController(VetService service)
        {
            Service = service;
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindAllVets()
        {
            var task = Service.FindAll();
            task.Wait();
            return JsonSerializer.Serialize(Results.Json(data: task.Result, statusCode: 200));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindVetByEmail(string email)
        {
            var task = Service.FindByEmail(email);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindVetByEmailShort(string email)
        {
            var task = Service.FindByEmailShort(email);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindVetByEmailAppointment(string email)
        {
            var task = Service.FindByEmailAppointment(email);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string RegisterVet(VetDTOregister dto)
        {
            var err = dto.Validate();
            if (err != null)
            {
                return JsonSerializer.Serialize(Results.Json(data: err.Message, statusCode: err.Code));
            }


            var task = Service.Register(dto);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 201),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string LoginVet(VetDTOloginOrChangePassword dto)
        {
            var err = dto.Validate();
            if (err != null)
            {
                return JsonSerializer.Serialize(Results.Json(data: err.Message, statusCode: err.Code));
            }

            var task = Service.Login(dto);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string ChangePasswordVet(VetDTOloginOrChangePassword dto)
        {
            var err = dto.Validate();
            if (err != null)
            {
                return JsonSerializer.Serialize(Results.Json(data: err.Message, statusCode: err.Code));
            }

            var task = Service.ChangePassword(dto);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string DeleteVet(string email)
        {
            var task = Service.Delete(email);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }
    }
}
