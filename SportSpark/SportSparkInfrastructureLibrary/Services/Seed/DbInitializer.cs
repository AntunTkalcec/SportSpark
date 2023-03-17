using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Interfaces.Seed;
using SportSparkInfrastructureLibrary.Data.SeedData;
using SportSparkInfrastructureLibrary.Database;

namespace SportSparkInfrastructureLibrary.Services.Seed
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }
        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<SportSparkDBContext>();
            context.Database.Migrate();
        }

        public void SeedData()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<SportSparkDBContext>();

            AddIfEmpty(context.Users, DbSeeder.Users);
            AddIfEmpty(context.EventTypes, DbSeeder.EventTypes);
            AddIfEmpty(context.EventRepeatTypes, DbSeeder.EventRepeatTypes);

            context.SaveChanges();

            AddIfEmpty(context.Events, DbSeeder.Events);
            AddIfEmpty(context.Friendships, DbSeeder.Friendships);

            context.SaveChanges();
        }

        private void AddIfEmpty<T>(DbSet<T> set, IEnumerable<T> entities) where T : class
        {
            if (!set.Any())
            {
                set.AddRange(entities);
            }
        }
    }
}
