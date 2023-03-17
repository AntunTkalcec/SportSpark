using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class EventRepository : BaseRepository<Event>
    {
        public EventRepository(SportSparkDBContext context) : base(context)
        {
        }
    }
}
