using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories;
using SportSparkInfrastructureLibrary.Services;

namespace SportSparkInfrastructureLibrary.Extensions
{
    public static class DependencyContainer
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SportSparkDBContext>(options => options.UseSqlServer(connectionString));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
