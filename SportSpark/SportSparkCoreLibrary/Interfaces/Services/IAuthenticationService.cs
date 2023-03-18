using SportSparkCoreSharedLibrary.DTOs;
using System.Security.Claims;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<UserDTO> RefreshTokenAsync(List<Claim> claims);
    }
}
