using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Get All Event Types
        [HttpGet]
        [Route("GetAllEventTypes")]

        public async Task<ActionResult<IEnumerable<EventType>>> GetAllEventTypes()
        {
            try
            {
                var eventTypes = await _context.EventTypes.Where(e => e.IsActive).ToListAsync();
                if(eventTypes.Count == 0)
                {
                    return NotFound("No active Event Types found.");
                }
            return Ok(eventTypes);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"An error occurred while fetching EventTypes.\n{e.Message}");

            }

        }

        // GET: api/Get Event Type By Id
        [HttpGet]
        [Route("GetEventTypeById")]
        public async Task<IActionResult> GetEventTypeById(int id)
        {
            var eventType = await _context.EventTypes.FirstOrDefaultAsync(e => e.EventTypeId == id && e.IsActive);
            if (eventType == null)
            {
                return NotFound();
            }
            return Ok(eventType);
        }

        // POST: api/Create Event Type
        [HttpPost]
        [Route("CreateEventType")]
        public async Task<IActionResult> CreateEventType([FromBody] EventType eventType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            eventType.IsActive = true;
            _context.EventTypes.Add(eventType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateEventType), new { id = eventType.EventTypeId }, eventType);
        }

        // PUT: api/Update Event Type
        [HttpPut]
        [Route("UpdateEventType")]
        public async Task<IActionResult> UpdateEventType(int id, [FromBody] EventType eventType)
        {
            if (id != eventType.EventTypeId)
            {
                return BadRequest();
            }

            var existingEventType = await _context.EventTypes.FindAsync(id);
            if (existingEventType == null || !existingEventType.IsActive)
            {
                return NotFound();
            }

            existingEventType.EventTypeName = eventType.EventTypeName;
            _context.EventTypes.Update(existingEventType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Delete EventType (Soft Delete)
        [HttpDelete]
        [Route("DeleteEventType")]
        public async Task<IActionResult> DeleteEventType(int id)
        {
            var eventType = await _context.EventTypes.FindAsync(id);
            if (eventType == null || !eventType.IsActive)
            {
                return NotFound();
            }

            eventType.IsActive = false; // Soft delete
            _context.EventTypes.Update(eventType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
