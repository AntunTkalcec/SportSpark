﻿using Microsoft.AspNetCore.Mvc;
using SportSparkAPI.Controllers.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSparkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ActionFilters.AuthorizationFilter()]
    public class EventController : BaseController
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
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

        [HttpGet("user/{id}")]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        public async Task<ActionResult<List<EventDTO>>> GetUserEventsById(int id)
        {
            try
            {
                return await _eventService.GetUserEventsAsync(id, UserId);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpPost("term/{radius}/{term}")]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        public async Task<ActionResult<List<EventDTO>>> GetEventsByTerm(double radius, LatLongWrapperDTO latLongWrapperDTO, string term)
        {
            try
            {
                return await _eventService.GetEventsByTermAsync(latLongWrapperDTO, radius, term, UserId);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpPost("{radius}")]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        public async Task<ActionResult<List<EventDTO>>> GetEventsInRadius(double radius, LatLongWrapperDTO latLongWrapperDTO)
        {
            try
            {
                var res = await _eventService.GetInRadiusAsync(latLongWrapperDTO, radius, UserId);

                return Ok(res.Where(_ => _.UserId != UserId).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(EventDTO eventDto)
        {
            try
            {
                await _eventService.CreateAsync(eventDto);

                return Created("", eventDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EventDTO eventDto)
        {
            try
            {
                await _eventService.UpdateAsync(id, eventDto);

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
                await _eventService.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseHelper(400, ex.Message));
            }
        }
    }
}
