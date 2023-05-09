using SportSparkCoreLibrary.Interfaces.Services.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IEventService : IBaseService<EventDTO>
    {
        Task<List<EventDTO>> GetInRadiusAsync(LatLongWrapperDTO wrapper, int radius);

        Task<List<EventDTO>> GetUserEventsAsync(int userId);
    }
}
