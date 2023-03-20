using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[ActionFilters.AuthorizationFilter()]
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

        [HttpPost]
        public async Task<ActionResult> Post(UserDTO userDto)
        {
            try
            {
                await _userService.CreateAsync(userDto);

                return Created("", userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UserDTO userDto)
        {
            try
            {
                await _userService.UpdateAsync(id, userDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }
    }
}
