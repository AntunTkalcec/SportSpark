using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FriendshipController : BaseController
    {
        private readonly IFriendshipService _friendshipService;
        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<FriendshipDTO>), 200)]
        public async Task<ActionResult<List<FriendshipDTO>>> GetReceivedFriendshipsAsync(int id)
        {
            try
            {
                return await _friendshipService.GetReceivedFriendshipsAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, FriendshipDTO friendshipDTO)
        {
            try
            {
                await _friendshipService.UpdateAsync(id, friendshipDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }
    }
}
