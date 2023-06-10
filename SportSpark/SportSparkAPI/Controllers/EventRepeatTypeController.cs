using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ActionFilters.AuthorizationFilter()]
    public class EventRepeatTypeController : BaseController
    {
        private readonly IEventRepeatTypeService _eventRepeatTypeService;
        public EventRepeatTypeController(IEventRepeatTypeService eventRepeatTypeService)
        {
            _eventRepeatTypeService = eventRepeatTypeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EventRepeatTypeDTO>), 200)]
        public async Task<ActionResult<List<EventRepeatTypeDTO>>> Get()
        {
            try
            {
                return await _eventRepeatTypeService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }
    }
}
