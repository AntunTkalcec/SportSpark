using SportSparkCoreLibrary.Entities;
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
