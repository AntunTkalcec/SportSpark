using SportSparkCoreSharedLibrary.DTOs;

namespace SportSpark.Services
{
    public interface IRestService
    {
        Task<UserDTO> GetLoggedInUser();
        Task<bool> SignInAsync(string emailOrUsername, string password);
        Task<bool> RegisterAsync(UserDTO userDTO);
        Task<List<EventDTO>> GetEventsNearUserAsync();
        Task<bool> UpdateUserInfoAsync(UserDTO userDTO);
        Task<List<EventDTO>> GetUserEventsAsync(int id);
        Task<List<EventDTO>> GetEventsByTermAsync(string term);

        bool CheckIfAuthenticated();
    }
}
