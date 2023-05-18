using SportSparkCoreLibrary.Entities;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class EventTypeRepository : BaseRepository<EventType>
    {
        public EventTypeRepository(SportSparkDBContext context) : base(context)
        {
        }
    }
}
