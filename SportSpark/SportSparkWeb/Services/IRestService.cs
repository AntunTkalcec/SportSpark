using SportSparkCoreSharedLibrary.Authentication.Models;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Models;

namespace SportSparkWeb.Services;

public interface IRestService
{
    Task<ApiResponseModel> SignInAsync(UserLogin userLogin);
    Task<ApiResponseModel> RegisterAsync(UserDTO userDTO);
    Task<UserDTO> GetUserAsync();
    Task<List<EventDTO>> GetEventsNearUserAsync(double? radius, LatLongWrapperDTO location);
    Task<UserDTO> GetUserByIdAsync(int id);
}
