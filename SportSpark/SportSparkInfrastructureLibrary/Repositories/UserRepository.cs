using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SportSparkDBContext context) : base(context)
        {
        }
    }
}
