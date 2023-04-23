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
    public class VetController
    {
        private readonly VetService Service;

        public VetController(VetService service)
        {
            Service = service;
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindAllVets()
        {
            Stopwatch.StartNew();
            var task = Service.FindAll();
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return new Result(200, JsonSerializer.Serialize(task.Result), time);
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindVetByEmail(string email)
        {
            Stopwatch.StartNew();
            var task = Service.FindByEmail(email);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindVetByEmailShort(string email)
        {
            Stopwatch.StartNew();
            var task = Service.FindByEmailShort(email);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindVetByEmailAppointment(string email)
        {
            Stopwatch.StartNew();
            var task = Service.FindByEmailAppointment(email);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result RegisterVet(VetDTOregister dto)
        {
            Stopwatch.StartNew();
            var err = dto.Validate();
            if (err != null)
            {
                var t = Stopwatch.GetTimestamp();
                return new Result(err.Code, err.Message, t);
            }


            var task = Service.Register(dto);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(201, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result LoginVet(VetDTOloginOrChangePassword dto)
        {
            Stopwatch.StartNew();
            var err = dto.Validate();
            if (err != null)
            {
                var t = Stopwatch.GetTimestamp();
                return new Result(err.Code, err.Message, t);
            }

            var task = Service.Login(dto);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result ChangeVetPassword(VetDTOloginOrChangePassword dto)
        {
            Stopwatch.StartNew();
            var err = dto.Validate();
            if (err != null)
            {
                var t = Stopwatch.GetTimestamp();
                return new Result(err.Code, err.Message, t);
            }

            var task = Service.ChangePassword(dto);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result DeleteVet(string email)
        {
            Stopwatch.StartNew();
            var task = Service.Delete(email);
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
