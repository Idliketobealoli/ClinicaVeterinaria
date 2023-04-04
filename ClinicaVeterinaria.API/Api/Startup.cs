using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.Schema;
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
            services.AddGraphQLServer().AddQueryType<Query>();

            string? connectionString = _configuration.GetConnectionString("default_connection");
            services.AddPooledDbContextFactory<ClinicaDBContext>(o => o.UseNpgsql(connectionString));
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
