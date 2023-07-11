using Blazored.LocalStorage;
using SportSparkCoreSharedLibrary.Authentication.Models;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SportSparkWeb.Services;

public class RestService : IRestService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly ISyncLocalStorageService _syncLocalStorageService;
    public RestService(HttpClient httpClient, ILocalStorageService localStorageService, ISyncLocalStorageService syncLocalStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _syncLocalStorageService = syncLocalStorageService;

        if (CheckIfAuthenticated())
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken());
        }
        
    }

    private string GetAccessToken()
    {
        return _syncLocalStorageService.GetItemAsString("access_token");
    }

    private bool CheckIfAuthenticated()
    {
        string accessToken = GetAccessToken();
        if (string.IsNullOrEmpty(accessToken))
        {
            return false;
        }

        JwtSecurityTokenHandler jwtHandler = new();
        JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(accessToken);
        
        if (jwtToken.ValidTo <= DateTime.UtcNow)
        {
            return false;
        }
        return true;
    }

    public async Task<UserDTO> GetUserAsync()
    {
        try
        {
            JwtSecurityTokenHandler jwtHandler = new();
            string token = GetAccessToken();
            if (string.IsNullOrEmpty(token))
            {
                return new();
            }
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            int userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            HttpResponseMessage response = await _httpClient.GetAsync($"User/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return new();
                }

                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"User/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default;
                }

                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApiResponseModel> RegisterAsync(UserDTO userDTO)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("User", userDTO);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponseModel()
                {
                    Status = 201,
                    Message = "Registration successful"
                };
            }
            else return await response.Content.ReadFromJsonAsync<ApiResponseModel>();
        }
        catch (Exception ex)
        {
            return new ApiResponseModel()
            {
                Status = 500,
                Message = "An internal error occurred."
            };
        }
    }

    public async Task<ApiResponseModel> SignInAsync(UserLogin userLogin)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Authentication/login", userLogin);

            if (response.IsSuccessStatusCode)
            {
                UserDTO? userDto = await response.Content.ReadFromJsonAsync<UserDTO>();
                AuthenticationInfo? authInfo = userDto?.AuthenticationInfo;

                JwtSecurityTokenHandler handler = new();
                var tokenS = handler.ReadToken(authInfo?.AccessToken) as JwtSecurityToken;

                await _localStorageService.SetItemAsStringAsync("access_token", authInfo?.AccessToken);
                await _localStorageService.SetItemAsStringAsync("refresh_token", authInfo?.RefreshToken);
                await _localStorageService.SetItemAsStringAsync("token_expiration", tokenS?.ValidTo.ToString());
                await _localStorageService.SetItemAsStringAsync("UserId", userDto.Id.ToString());

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authInfo?.AccessToken);

                return new ApiResponseModel()
                {
                    Status = 200,
                    Message = "Login successful"
                };
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<ApiResponseModel>();
            }
        }
        catch (Exception ex)
        {
            return new ApiResponseModel()
            {
                Status = 500,
                Message = "An internal error occurred."
            };
        }
    }

    public async Task<List<EventDTO>> GetEventsNearUserAsync(double? radius, LatLongWrapperDTO location)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Event/{radius}", location);

            if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
            {
                return await response.Content.ReadFromJsonAsync<List<EventDTO>>();
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
