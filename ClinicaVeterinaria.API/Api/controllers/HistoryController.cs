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
    public class HistoryController
    {
        private readonly HistoryService Service;

        public HistoryController(HistoryService service)
        {
            Service = service;
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindAllHistories()
        {
            Stopwatch.StartNew();
            var task = Service.FindAll();
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return new Result(200, JsonSerializer.Serialize(task.Result), time);
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindHistoryByPetId(Guid id)
        {
            Stopwatch.StartNew();
            var task = Service.FindByPetId(id);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindHistoryByPetIdVaccinesOnly(Guid id)
        {
            Stopwatch.StartNew();
            var task = Service.FindByPetIdVaccinesOnly(id);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result FindHistoryByPetIdAilmTreatOnly(Guid id)
        {
            Stopwatch.StartNew();
            var task = Service.FindByPetIdAilmTreatOnly(id);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(200, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result AddVaccine(Guid id, VaccineDTO vaccine)
        {
            Stopwatch.StartNew();
            var err = vaccine.Validate();
            if (err != null)
            {
                var t = Stopwatch.GetTimestamp();
                return new Result(err.Code, err.Message, t);
            }

            var task = Service.AddVaccine(id, vaccine);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(201, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public Result AddAilmentTreatment(Guid id, string ailment, string treatment)
        {
            Stopwatch.StartNew();
            var task = Service.AddAilmentTreatment(id, ailment, treatment);
            task.Wait();
            var time = Stopwatch.GetTimestamp();

            return task.Result.Match
                (
                onSuccess: x => new Result(201, JsonSerializer.Serialize(x), time),
                onError: x => new Result(x.Code, x.Message, time)
                );
        }
    }
}
