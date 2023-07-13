using SportSparkCoreLibrary.Interfaces.Services.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IEventService : IBaseService<EventDTO>
    {
        Task<List<EventDTO>> GetInRadiusAsync(LatLongWrapperDTO wrapper, double radius, int userId);

        Task<List<EventDTO>> GetUserEventsAsync(int userId, int loggedInUserId);

        Task<List<EventDTO>> GetEventsByTermAsync(LatLongWrapperDTO wrapper, double radius, string term, int userId);

        Task<List<EventDTO>> GetUserFriendEventsAsync(int userId);
    }
}
