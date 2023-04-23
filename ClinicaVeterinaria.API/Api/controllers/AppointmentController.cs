using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.schema;
using ClinicaVeterinaria.API.Api.services;
using System.Diagnostics;
using System.Text.Json;

namespace ClinicaVeterinaria.API.Api.controllers
{
    [ExtendObjectType(typeof(Query))]
    public class AppointmentController
    {
        private readonly AppointmentService Service;

        public AppointmentController(AppointmentService service)
        {
            Service = service;
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindAllAppointments()
        {
            Stopwatch.StartNew();
            var task = Service.FindAll();
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return new Result(200, JsonSerializer.Serialize(task.Result), time);
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindAppointmentById(Guid id)
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
        public Result CreateAppointment(Appointment appointment)
        {
            Stopwatch.StartNew();
            var task = Service.Create(appointment);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(201, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result DeleteAppointment(Guid id)
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
