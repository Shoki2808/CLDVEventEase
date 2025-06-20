using Microsoft.AspNetCore.Mvc;
using EventEaseAPI.Interfaces;
using EventEaseAPI.Enums;
using EventEaseAPI.DTOs.Event;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("GetAllEvents")]
        public async Task<ActionResult<IEnumerable<EventReadDto>>> GetAllEvents()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        [HttpGet]
        [Route("GetEventById")]
        public async Task<ActionResult<EventReadDto>> GetEventById(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null) return NotFound(new { message = $"Event with ID {id} not found." });
            return Ok(ev);
        }

        [HttpPost]
        [Route("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (result, data) = await _eventService.CreateAsync(dto);

            return result switch
            {
                CreateEventResult.Success => CreatedAtAction(nameof(GetEventById), new { id = data.EventId }, data),
                CreateEventResult.VenueNotActive => BadRequest(new { message = "The selected venue is Booked." }),
                CreateEventResult.VenueAlreadyBooked => BadRequest(new { message = "The venue is already booked." }),
                _ => StatusCode(500, new { message = "An unexpected error occurred." })
            };
        }

        [HttpPut]
        [Route("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _eventService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { message = $"Event with ID {id} not found." });
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var success = await _eventService.DeleteAsync(id);
            if (!success) return NotFound(new { message = $"Event with ID {id} not found." });
            return NoContent();
        }
    }
}
