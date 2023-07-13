using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Repositories;

public interface IEventRepository : IBaseRepository<Event>
{
    Task<List<int>> GetEventsByLocation(LatLongWrapperDTO wrapper, double radius);
    Task<Event> GetByIdDetailedAsync(int id);
}
