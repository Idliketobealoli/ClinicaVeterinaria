using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.model;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.API.Api.repositories
{
    public class VetRepository
    {
        private readonly IDbContextFactory<ClinicaDBContext> ContextFactory;

        public VetRepository(IDbContextFactory<ClinicaDBContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<List<Vet>> FindAll()
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var vets = await context.Vets.ToListAsync();
            return vets ?? new();
        }

        public async Task<Vet?> FindById(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            return await context.Vets.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Vet> Create(Vet vet)
        {
            using (ClinicaDBContext context = ContextFactory.CreateDbContext())
            {
                context.Vets.Add(vet);
                await context.SaveChangesAsync();
                return vet;
            }
        }

        public async Task<Vet?> Update(Guid id, Vet vet)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var foundVet = context.Vets.FirstOrDefault(u => u.Id == id);
            if (foundVet != null)
            {
                vet.Id = foundVet.Id;
                context.Vets.Update(vet);
                await context.SaveChangesAsync();
                return vet;
            }
            return null;
        }

        public async Task<Vet?> Delete(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var foundVet = context.Vets.FirstOrDefault(u => u.Id == id);
            if (foundVet != null)
            {
                context.Vets.Remove(foundVet);
                await context.SaveChangesAsync();
                return foundVet;
            }
            return null;
        }
    }
}