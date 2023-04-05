using ClinicaVeterinaria.API.Api.db;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IDbContextFactory<ClinicaDBContext> contextFactory =
                    scope.ServiceProvider.GetRequiredService<IDbContextFactory<ClinicaDBContext>>();

                using ClinicaDBContext context = contextFactory.CreateDbContext();
                context.Database.Migrate();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}