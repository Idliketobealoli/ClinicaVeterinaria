using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.schema;
using ClinicaVeterinaria.API.Api.services;
using ClinicaVeterinaria.API.Api.validators;
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
        public string FindAll()
        {
            var task = Service.FindAll();
            task.Wait();
            return JsonSerializer.Serialize(Results.Json(data: task.Result, statusCode: 200));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindByPetId(Guid id)
        {
            var task = Service.FindByPetId(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindByPetIdVaccinesOnly(Guid id)
        {
            var task = Service.FindByPetIdVaccinesOnly(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string FindByPetIdAilmTreatOnly(Guid id)
        {
            var task = Service.FindByPetIdAilmTreatOnly(id);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string AddVaccine(Guid id, VaccineDTO vaccine)
        {
            var err = vaccine.Validate();
            if (err != null)
            {
                return JsonSerializer.Serialize(Results.Json(data: err.Message, statusCode: err.Code));
            }

            var task = Service.AddVaccine(id, vaccine);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }

        [UseDbContext(typeof(ClinicaDBContext))]
        public string AddAilmentTreatment(Guid id, string ailment, string treatment)
        {
            var task = Service.AddAilmentTreatment(id, ailment, treatment);
            task.Wait();
            return JsonSerializer.Serialize(task.Result.Match
                (
                onSuccess: x => Results.Json(data: x, statusCode: 200),
                onError: x => Results.Json(data: x.Message, statusCode: x.Code)
                ));
        }
    }
}
