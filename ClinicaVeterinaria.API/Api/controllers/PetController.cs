using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.schema;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;
using System.Text.Json;

namespace ClinicaVeterinaria.API.Api.controllers
{
    [ExtendObjectType(typeof(Query))]
    public class PetController
    {
        private readonly PetService Service;

        public PetController(PetService service)
        {
            Service = service;
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return JsonSerializer.Serialize(Results.Json(data: task.Result, statusCode: 200));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindById(Guid id)
        {
            var task = Service.FindById(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindByIdNoPhoto(Guid id)
        {
            var task = Service.FindByIdNoPhoto(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string Create(PetDTOcreate dto)
        {
            var err = dto.Validate();
            if (err != null)
            {
                return JsonSerializer.Serialize(Results.Json(data: err.Message, statusCode: err.Code));
            }

            var task = Service.Create(dto);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 201),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string Update(PetDTOupdate dto)
        {
            var err = dto.Validate();
            if (err != null)
            {
                return JsonSerializer.Serialize(Results.Json(data: err.Message, statusCode: err.Code));
            }

            var task = Service.Update(dto);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string Delete(Guid id)
        {
            var task = Service.Delete(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }
    }
}
