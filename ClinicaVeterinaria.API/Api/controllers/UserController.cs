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
    public class UserController
    {
        private readonly UserService Service;

        public UserController(UserService service)
        {
            Service = service;
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindAllUsers()
        {
            Stopwatch.StartNew();
            var task = Service.FindAll();
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return new Result(200, JsonSerializer.Serialize(task.Result), time);
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindUserByEmail(string email)
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
        public Result FindUserByEmailShort(string email)
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
        public Result RegisterUser(UserDTOregister dto)
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
        public Result LoginUser(UserDTOloginOrChangePassword dto)
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
        public Result ChangeUserPassword(UserDTOloginOrChangePassword dto)
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
        public Result DeleteUser(string email)
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
