using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreSharedLibrary.Authentication.Models;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IUserService userService, ITokenService tokenService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserLogin userLogin)
        {
            try
            {
                var user = await _userService.UserValid(userLogin.EmailOrUserName, userLogin.Password);
                if (user is not null)
                {
                    var userDto = _userService.Login(user);
                    return Ok(userDto);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpPost("token/refresh")]
        public async Task<ActionResult<UserDTO>> RefreshToken([FromBody] string refreshToken)
        {
            var claims = _tokenService.GetClaimsFromJwt(refreshToken);
            var userDto = await _authenticationService.RefreshTokenAsync(claims);
            if (userDto is not null)
            {
                return Ok(userDto);
            }
            return Unauthorized();
        }
    }
}
