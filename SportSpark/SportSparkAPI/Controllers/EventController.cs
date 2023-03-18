using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SportSparkAPI.ActionFilters.AuthorizationFilter()]
    public class EventController : BaseController
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EventDTO), 200)]
        public async Task<ActionResult<List<EventDTO>>> Get()
        {
            try
            {
                return await _eventService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventDTO), 200)]
        public async Task<ActionResult<EventDTO>> GetById(int id)
        {
            try
            {
                return await _eventService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }
    }
}
