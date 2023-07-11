using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkInfrastructureLibrary.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SportSparkWeb.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ITokenService _tokenService;
    public CustomAuthStateProvider(ILocalStorageService localstorage, ITokenService tokenService)
    {
        _localStorageService = localstorage;
        _tokenService = tokenService;
    }

    public async Task<string> GetAccessToken()
    {
        return await _localStorageService.GetItemAsStringAsync("access_token");
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        JwtSecurityTokenHandler jwtHandler = new();
        string token = await GetAccessToken();

        if (!string.IsNullOrEmpty(token))
        {
            var jwtToken = jwtHandler.ReadJwtToken(token);
            List<Claim> claims = GetClaimsFromJwt(token);

            if (jwtToken.ValidTo >= DateTime.Now && claims is not null)
            {
                identity = new ClaimsIdentity(claims, "jwt");
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private List<Claim> GetClaimsFromJwt(string jwt)
    {
        try
        {
            JwtSecurityTokenHandler handler = new();

            handler.ValidateToken(jwt, TokenValidationConfiguration.GetTokenValidationParameters("SportSpark",
                "SportSpark", "SportSparkSuperSecretKey"), out SecurityToken securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken)
            {
                return jwtSecurityToken?.Claims?.ToList();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
}
