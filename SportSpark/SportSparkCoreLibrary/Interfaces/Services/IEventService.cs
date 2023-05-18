using SportSparkCoreLibrary.Interfaces.Services.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IEventService : IBaseService<EventDTO>
    {
        Task<List<EventDTO>> GetInRadiusAsync(LatLongWrapperDTO wrapper, double radius);

        Task<List<EventDTO>> GetUserEventsAsync(int userId);

        Task<List<EventDTO>> GetEventsByTermAsync(LatLongWrapperDTO wrapper, double radius, string term);
    }
}
