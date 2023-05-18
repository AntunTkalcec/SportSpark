using SportSparkCoreLibrary.Entities;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class EventRepeatTypeRepository : BaseRepository<EventRepeatType>
    {
        public EventRepeatTypeRepository(SportSparkDBContext context) : base(context)
        {
            
        }
    }
}
