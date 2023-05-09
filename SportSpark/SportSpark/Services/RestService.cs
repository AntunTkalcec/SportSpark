using Newtonsoft.Json;
using SportSparkCoreSharedLibrary.Authentication.Models;
using SportSparkCoreSharedLibrary.DTOs;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace SportSpark.Services
{
    public class RestService : IRestService
    {
        private static readonly string AccessTokenKey = "access_token";
        private static readonly string RefreshTokenKey = "refresh_token";
        private static readonly string TokenExpirationKey = "token_expiration";

        private readonly HttpClient _httpClient;
        private readonly IHttpsClientHandlerService _httpsClientHandlerService;

        public RestService(IHttpsClientHandlerService httpsClientHandlerService)
        {
            _httpsClientHandlerService = httpsClientHandlerService;
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            if (handler != null)
            {
                _httpClient = new HttpClient(handler);
            }
            else
            {
                _httpClient = new HttpClient();
            }

            if (CheckIfAuthenticated())
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken());
            }
        }

        private async Task<UserDTO> GetUser()
        {
            try
            {
                JwtSecurityTokenHandler jwtHandler = new();
                var jwtToken = jwtHandler.ReadJwtToken(GetAccessToken());
                int id = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var response = await _httpClient.GetAsync($"{SettingsManager.BaseURL}/User/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDTO>();
                }
                else
                {
                    return new UserDTO();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new UserDTO();
            }
        }

        public static string GetAccessToken() => Preferences.Get(AccessTokenKey, "");

        public static string GetRefreshToken() => Preferences.Get(RefreshTokenKey, "");

        public static DateTime? GetTokenExpiration() => Preferences.Get(TokenExpirationKey, DateTime.MinValue);

        public bool CheckIfAuthenticated()
        {
            if (string.IsNullOrEmpty(GetAccessToken()))
            {
                return false;
            }
            JwtSecurityTokenHandler jwtHandler = new();
            var jwtToken = jwtHandler.ReadJwtToken(GetAccessToken());

            if (jwtToken.ValidTo <= DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        public async Task<UserDTO> GetLoggedInUser()
        {
            return await GetUser();
        }

        public async Task<bool> SignInAsync(string usernameOrEmail, string password)
        {
            UserLogin userLogin = new()
            {
                EmailOrUserName = usernameOrEmail,
                Password = password
            };

            try
            {
                string json = JsonConvert.SerializeObject(userLogin);
                StringContent content = new(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{SettingsManager.BaseURL}/Authentication/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    UserDTO userDto = JsonConvert.DeserializeObject<UserDTO>(responseContent);
                    AuthenticationInfo authInfo = userDto.AuthenticationInfo;

                    JwtSecurityTokenHandler handler = new();
                    var tokenS = handler.ReadToken(authInfo.AccessToken) as JwtSecurityToken;

                    Preferences.Set(AccessTokenKey, authInfo.AccessToken);
                    Preferences.Set(RefreshTokenKey, authInfo.RefreshToken);
                    Preferences.Set(TokenExpirationKey, tokenS.ValidTo);

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authInfo.AccessToken);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> RegisterAsync(UserDTO userDTO)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(userDTO);
                StringContent content = new(jsonData, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{SettingsManager.BaseURL}/User", content);
                if (response.IsSuccessStatusCode)
                    return true;
                else 
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<EventDTO>> GetEventsNearUserAsync()
        {
            //todo get only events inside specified radius
            var response = await _httpClient.GetAsync($"{SettingsManager.BaseURL}/Event");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EventDTO>>();
            }
            else
            {
                //Toast.Make($"{response.Content}");
                return new List<EventDTO>();
            }
        }

        public async Task<bool> UpdateUserInfoAsync(UserDTO userDTO)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(userDTO);
                StringContent content = new(jsonData, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{SettingsManager.BaseURL}/User/{userDTO.Id}", content);
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<EventDTO>> GetUserEventsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{SettingsManager.BaseURL}/Event/user/{id}");
            if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
            {
                return await response.Content.ReadFromJsonAsync<List<EventDTO>>();
            }
            else
            {
                return new List<EventDTO>();
            }
        }
    }
}
