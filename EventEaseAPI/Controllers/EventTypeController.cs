using EventEaseAPI.DTOs.EventType;
using EventEaseAPI.Interfaces;
using EventEaseAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypeController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        [HttpGet]
        [Route("GetAllEventTypes")]
        public async Task<ActionResult<IEnumerable<EventTypeReadDto>>> GetAllEventTypes()
        {
            var eventTypes = await _eventTypeService.GetAllAsync();
            return Ok(eventTypes);
        }

        [HttpGet]
        [Route("GetEventTypeById")]
        public async Task<ActionResult<EventTypeReadDto>> GetEventTypeById(int id)
        {
            var eventType = await _eventTypeService.GetByIdAsync(id);
            if (eventType == null)
                return NotFound(new { message = $"EventType with ID {id} not found." });
            return Ok(eventType);
        }

        [HttpPost]
        [Route("CreateEventType")]
        public async Task<ActionResult<EventTypeReadDto>> CreateEventType([FromBody] EventTypeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdEventType = await _eventTypeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetEventTypeById), new { id = createdEventType.EventTypeId }, createdEventType);
        }

        [HttpPut]
        [Route("UpdateEventType")]
        public async Task<IActionResult> UpdateEventType(int id, [FromBody] EventTypeUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _eventTypeService.UpdateAsync(id, dto);
            if (!success)
                return NotFound(new { message = $"EventType with ID {id} not found." });
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteEventType")]
        public async Task<IActionResult> DeleteEventType(int id)
        {
            var success = await _eventTypeService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = $"EventType with ID {id} not found." });
            return NoContent();
        }
    }
}
