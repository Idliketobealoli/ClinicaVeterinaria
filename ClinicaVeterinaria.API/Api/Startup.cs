using ClinicaVeterinaria.API.Api.controllers;
using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.repositories;
using ClinicaVeterinaria.API.Api.schema;
using ClinicaVeterinaria.API.Api.services;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<VetController>()
                .AddType<UserController>()
                .AddType<PetController>()
                .AddType<HistoryController>()
                .AddType<AppointmentController>();

            string? connectionString = _configuration.GetConnectionString("default_connection");
            services.AddPooledDbContextFactory<ClinicaDBContext>(o => o.UseNpgsql(connectionString));

            services.AddSingleton<UserRepository>();
            services.AddSingleton<VetRepository>();
            services.AddSingleton<AppointmentRepository>();
            services.AddSingleton<HistoryRepository>();
            services.AddSingleton<VaccineRepository>();
            services.AddSingleton<PetRepository>();
            services.AddSingleton<UserService>();
            services.AddSingleton<VetService>();
            services.AddSingleton<AppointmentService>();
            services.AddSingleton<HistoryService>();
            services.AddSingleton<PetService>();
            services.AddSingleton<UserController>();
            services.AddSingleton<VetController>();
            services.AddSingleton<AppointmentController>();
            services.AddSingleton<HistoryController>();
            services.AddSingleton<PetController>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
