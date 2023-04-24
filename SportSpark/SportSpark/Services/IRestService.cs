using SportSparkCoreSharedLibrary.DTOs;

namespace SportSpark.Services
{
    public interface IRestService
    {
        Task<bool> SignInAsync(string emailOrUsername, string password);
        Task<bool> RegisterAsync(UserDTO userDTO);
        Task<List<EventDTO>> GetEventsNearUserAsync();

        bool CheckIfAuthenticated();
    }
}
