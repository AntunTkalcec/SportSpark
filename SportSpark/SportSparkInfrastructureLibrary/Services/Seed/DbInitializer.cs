using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Interfaces.Seed;
using SportSparkInfrastructureLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void SeedData(bool useAdditionalSeeds)
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<SportSparkDBContext>();

        }
    }
}
