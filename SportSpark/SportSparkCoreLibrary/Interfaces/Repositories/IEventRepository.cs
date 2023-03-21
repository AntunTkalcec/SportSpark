using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Repositories;

public interface IEventRepository : IBaseRepository<Event>
{
    Task<List<Event>> GetEventsByLocation(LatLongWrapperDTO wrapper, int radius);
}
