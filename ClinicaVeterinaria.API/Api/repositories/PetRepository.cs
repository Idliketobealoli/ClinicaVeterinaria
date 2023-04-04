using ClinicaVeterinaria.API.Api.db;
using ClinicaVeterinaria.API.Api.model;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.API.Api.repositories
{
    public class PetRepository
    {
        private readonly IDbContextFactory<ClinicaDBContext> ContextFactory;

        public PetRepository(IDbContextFactory<ClinicaDBContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<List<Pet>> FindAll()
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var pets = await context.Pets.ToListAsync();
            return pets ?? new();
        }

        public async Task<Pet?> FindById(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            return await context.Pets.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Pet> Create(Pet pet)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            context.Pets.Add(pet);
            await context.SaveChangesAsync();

            return pet;
        }

        public async Task<Pet?> Update(Guid id, Pet pet)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var found = context.Pets.FirstOrDefault(u => u.Id == id);
            if (found != null)
            {
                pet.Id = found.Id;
                context.Pets.Update(pet);
                await context.SaveChangesAsync();

                return pet;
            }
            return null;
        }

        public async Task<Pet?> Delete(Guid id)
        {
            using ClinicaDBContext context = ContextFactory.CreateDbContext();
            var found = context.Pets.FirstOrDefault(u => u.Id == id);
            if (found != null)
            {
                context.Pets.Remove(found);
                await context.SaveChangesAsync();

                return found;
            }
            return null;
        }
    }
}
