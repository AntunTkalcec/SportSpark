using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            try
            {
                return await _userService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            try
            {
                return await _userService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }
    }
}
