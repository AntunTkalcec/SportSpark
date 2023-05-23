using SportSparkCoreSharedLibrary.DTOs;

namespace SportSpark.Services
{
    public interface IRestService
    {
        Task<UserDTO> GetLoggedInUser();
        Task<bool> SignInAsync(string emailOrUsername, string password);
        Task<bool> RegisterAsync(UserDTO userDTO);
        Task<List<EventDTO>> GetEventsNearUserAsync(double? radius, LatLongWrapperDTO location);
        Task<bool> UpdateUserInfoAsync(UserDTO userDTO);
        Task<List<EventDTO>> GetUserEventsAsync(int id);
        Task<List<EventDTO>> GetEventsByTermAsync(double? radius, LatLongWrapperDTO location, string term);
        Task<bool> AddAsFriendAsync(int userId);
        Task<List<EventTypeDTO>> GetEventTypesAsync();
        Task<List<EventRepeatTypeDTO>> GetEventRepeatTypesAsync();
        Task CreateNewEventAsync(EventDTO eventDTO);
        Task<List<FriendshipDTO>> GetReceivedFriendshipsForUserAsync(int userId);
        Task<bool> UpdateFriendshipStatus(FriendshipDTO friendshipDTO);
        Task CreateNewProfilePictureAsync(DocumentDTO documentDTO);
        Task RateUserAsync(int userId, int rating);

        bool CheckIfAuthenticated();
    }
}
