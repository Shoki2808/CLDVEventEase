using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EventController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [Route("GetAllEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            try
            {
                var events = await context.Events.Where(e => e.IsActive).ToListAsync();
                if (events.Count == 0)
                {
                    return NotFound("No active events found.");
                }

                return Ok(events);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching events.\n{e.Message}");
            }

        }


        [HttpGet]
        [Route("GetEventById")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
            try
            {
                var eventItem = await context.Events.FirstOrDefaultAsync(e => e.EventId == id);

                if (!await EventExists(id))
                {
                    return NotFound($"The Event with Event ID {id} does not exist");
                }
                else if (!eventItem.IsActive)
                {
                    return NotFound($"No active events found for Event ID {id}");
                }


                return Ok(eventItem);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching the event specified. Event ID {id}\n{e.Message}");
            }

        }

        [HttpPost]
        [Route("CreateEvent")]
        public async Task<ActionResult<Event>> CreateEvent(Event eventItem)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var venueExists = await context.Venues.AnyAsync(v => v.VenueId == eventItem.VenueId && v.IsActive);
                var eventTypeExists = await context.EventTypes.AnyAsync(e => e.EventTypeId == eventItem.EventTypeId && e.IsActive);

                if (!venueExists && !eventTypeExists)
                {
                    return BadRequest($"The Event Type ID ({eventItem.EventTypeId}) and the Venue ID ({eventItem.VenueId}) specified do not exist.");
                }
                else if (!venueExists)
                {
                    return BadRequest($"The specified venue with ID ({eventItem.VenueId}) does not exist.");
                }
                else if (!eventTypeExists)
                {
                    return BadRequest($"The specified event type with ID ({eventItem.EventTypeId}) does not exist.");
                }

                eventItem.IsActive = true;
                context.Events.Add(eventItem);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEventById), new { id = eventItem.EventId }, eventItem);
            }
            catch (DbUpdateException dbue)
            {
                // Logs can be added here
                return StatusCode(500, $"An error occurred while saving the event to the database.\n{dbue.Message}");
            }
            catch (Exception e)
            {
                // Logs can be added here
                return StatusCode(500, $"An unexpected error occurred. Please try again later.\n{e.Message}");
            }
        }



        [HttpPut]
        [Route("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(int id, Event eventItem)
        {
            try
            {

                if (id != eventItem.EventId)
                {
                    return BadRequest("Event ID mismatch.");
                }
                else if (!await EventExists(id))
                {
                    return NotFound($"The Event ID specified does not exist. Event ID: {id}");
                }
                else if (!eventItem.IsActive)
                {
                    return NotFound($"No Active Events found for ID: {id}");
                }

                var venueExists = await context.Venues.AnyAsync(v => v.VenueId == eventItem.VenueId && v.IsActive);
                var eventTypeExists = await context.EventTypes.AnyAsync(e => e.EventTypeId == eventItem.EventTypeId && e.IsActive);

                if (!venueExists && !eventTypeExists)
                {
                    return BadRequest($"The Event Type ID ({eventItem.EventTypeId}) and the Venue ID ({eventItem.VenueId}) specified do not exist.");
                }
                else if (!venueExists)
                {
                    return BadRequest($"The specified venue with ID ({eventItem.VenueId}) does not exist.");
                }
                else if (!eventTypeExists)
                {
                    return BadRequest($"The specified event type with ID ({eventItem.EventTypeId}) does not exist.");
                }


                context.Entry(eventItem).State = EntityState.Modified;
                await context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException dbce)
            {
                return StatusCode(500, $"Concurrency error while updating.\n{dbce.Message}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An unxpcected error occurred.\n{e.Message}");
            }

            return NoContent();
        }

        //Soft Delete
        [HttpDelete]
        [Route("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var eventItem = await context.Events.FindAsync(id);
                bool hasBookings = await context.Bookings.AnyAsync(b => b.EventId == id);

                if (!await EventExists(id) || eventItem == null)
                {
                    return NotFound($"The Event ID specified does not exist. Event ID: {id}");
                }
                else if (!eventItem.IsActive)
                {
                    return NotFound($"No Active Events found for ID: {id}");
                }
                else if (hasBookings)
                {
                    return BadRequest("Cannot delete event with existing bookings.");
                }

                eventItem.IsActive = false;
                context.Update(eventItem);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                return StatusCode(500, $"Concurrency error while updating.\n{dbce.Message}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An unxpcected error occurred.\n{e.Message}");

            }
        }


        private async Task<bool> EventExists(int id)
        {
            var eventItem = await context.Events.AnyAsync(e => e.EventId == id);
            return eventItem;
        }
    }
}
