using AutoMapper;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using System.Security.Claims;
using SportSparkCoreLibrary.Entities;

namespace SportSparkInfrastructureLibrary.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public AuthenticationService(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    public async Task<UserDTO> RefreshTokenAsync(List<Claim> claims)
    {
        if (claims is not null)
        {
            var userId = claims.SingleOrDefault(c => c.Type == "UserId");
            if (userId is not null)
            {
                var user = _mapper.Map<User>(await _userService.GetByIdAsync(int.Parse(userId.Value)));
                if (user is not null)
                {
                    var userDto = _userService.Login(user);
                    return userDto;
                }
                return null;
            }
        }
        return null;
    }
}
