using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventVendorController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EventVendorController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [Route("GetAllEventVendors")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventVendors()
        {
            try
            {
                var eventVendors = await context.EventVendors.Where(e => e.IsActive).ToListAsync();
                if (eventVendors.Count == 0)
                {
                    return NotFound("No active event vendors found");
                }

                return Ok(eventVendors);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching events.\n{e.Message}");
            }
        }


        [HttpGet]
        [Route("GetEventVendorById")]
        public async Task<ActionResult<EventVendor>> GetEventVendorById(int id)
        {
            try
            {
                var eventVendor = await context.EventVendors.FirstOrDefaultAsync(e => e.EventVendorId == id);
                if (!await EventVendorExists(id))
                {
                    return NotFound($"The Event with Event ID {id} does not exist");
                }
                else if (!eventVendor.IsActive)
                {
                    return NotFound($"No active events found for Event ID {id}");
                }
                return Ok(eventVendor);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching the event vendor specified. Event ID {id}\n{e.Message}");
            }
        }


        [HttpPost]
        [Route("CreateEventVendor")]
        public async Task<ActionResult<EventVendor>> CreateEventVendor([FromBody] EventVendor eventVendor)
        {


            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var eventExists = await context.Events.AnyAsync(e => e.EventId == eventVendor.EventId && e.IsActive);

                if (!eventExists)
                {
                    return BadRequest($"The specified event with ID ({eventVendor.EventId}) does not exist.");
                }

                eventVendor.IsActive = true;
                context.EventVendors.Add(eventVendor);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEventVendorById), new { id = eventVendor.EventVendorId }, eventVendor);
            }
            catch (DbUpdateException dbue)
            {
                // Logs can be added here
                return StatusCode(500, $"An error occurred while saving the Event Vendor to the database.\n{dbue.Message}");
            }
            catch (Exception e)
            {
                // Logs can be added here
                return StatusCode(500, $"An unexpected error occurred. Please try again later.\n{e.Message}");
            }
        }


        [HttpPut]
        [Route("UpdateEventVendor")]
        public async Task<IActionResult> UpdateEventVendor(int id, [FromBody] EventVendor eventVendor)
        {
            try
            {


                if (id != eventVendor.EventVendorId)
                {
                    return BadRequest("Event Vendor ID mismatch.");
                }
                else if (!await EventVendorExists(id))
                {
                    return NotFound($"The Event Vendor ID specified does not exist. Event ID: {id}");
                }
                else if (!eventVendor.IsActive)
                {
                    return NotFound($"No Active Event Vendors found for ID: {id}");
                }

                var eventExists = await context.Events.AnyAsync(e => e.EventId == eventVendor.EventId && e.IsActive);

                if (!eventExists)
                {
                    return BadRequest($"The specified event with ID ({eventVendor.EventId}) does not exist.");
                }


                context.Entry(eventVendor).State = EntityState.Modified;
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



        [HttpDelete]
        [Route("DeleteEventVendor")]
        public async Task<IActionResult> DeleteEventVendor(int id)
        {
            try
            {
                var eventItem = await context.Events.FindAsync(id);
                bool hasBookings = await context.Bookings.AnyAsync(b => b.EventId == id);

                if (!await EventVendorExists(id) || eventItem == null)
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
        private async Task<bool> EventVendorExists(int id)
        {
            return await context.EventVendors.AnyAsync(v => v.EventVendorId == id);
        }
    }
}
