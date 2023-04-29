using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.schema;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;
using System.Diagnostics;
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
        public Result FindAllPets()
        {
            Stopwatch.StartNew();
            var task = Service.FindAll();
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return new Result(200, JsonSerializer.Serialize(task.Result), time);
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindPetById(Guid id)
        {
            Stopwatch.StartNew();
            var task = Service.FindById(id);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result CreatePet(PetDTOcreate dto)
        {
            Stopwatch.StartNew();
            var err = dto.Validate();
            if (err != null)
            {
                var t = Stopwatch.GetTimestamp();
                return new Result(err.Code, err.Message, t);
            }

            var task = Service.Create(dto);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(201, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result UpdatePet(PetDTOupdate dto)
        {
            Stopwatch.StartNew();
            var err = dto.Validate();
            if (err != null)
            {
                var t = Stopwatch.GetTimestamp();
                return new Result(err.Code, err.Message, t);
            }

            var task = Service.Update(dto);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result DeletePet(Guid id)
        {
            Stopwatch.StartNew();
            var task = Service.Delete(id);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }
    }
}
