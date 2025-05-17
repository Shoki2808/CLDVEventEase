using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly ApplicationDbContext context;


        public VenueController(ApplicationDbContext context)
        {
            this.context = context;

        }


        [HttpGet]
        [Route("GetAllVenues")]
        public async Task<ActionResult<IEnumerable<Venue>>> GetAllVenues()
        {
            try
            {
                var venues = await context.Venues.Where(v => v.IsActive).ToListAsync();
                if (venues.Count == 0)
                {
                    return NotFound("No active Venues found.");
                }

                return Ok(venues);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching Venues.\n{e.Message}");
            }

        }


        [HttpGet]
        [Route("GetVenueById")]
        public async Task<ActionResult<Venue>> GetVenueById(int id)
        {
            try
            {
                var venue = await context.Venues.FirstOrDefaultAsync(v => v.VenueId == id);

                if (!await VenueExists(id))
                {
                    return NotFound($"The venue with venue ID {id} does not exist");
                }
                else if (!venue.IsActive)
                {
                    return NotFound($"No active venues found for venue ID {id}");
                }


                return Ok(venue);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching the venue specified. venue ID {id}\n{e.Message}");
            }

        }


        [HttpPost]
        [Route("CreateVenue")]
        public async Task<ActionResult<Venue>> CreateVenue([FromBody] Venue venue)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var eventTypeExists = await context.EventTypes.AnyAsync(e => e.EventTypeId == venue.EventTypeId && e.IsActive);

                //if (!eventTypeExists)
                //{
                //    return BadRequest($"The specified event type with ID ({venue.EventTypeId}) does not exist.");
                //}

                venue.IsActive = true;
                context.Venues.Add(venue);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetVenueById), new { id = venue.VenueId }, venue);
            }
            catch (DbUpdateException dbue)
            {

                return StatusCode(500, $"An error occurred while saving the event to the database.\n{dbue.Message}");
            }
            catch (Exception e)
            {

                return StatusCode(500, $"An unexpected error occurred. Please try again later.\n{e.Message}");
            }
        }


        [HttpPut]
        [Route("UpdateVenue")]
        public async Task<IActionResult> UpdateVenue(int id, [FromBody] Venue venue)
        {
            try
            {

                if (id != venue.VenueId)
                {
                    return BadRequest("Venue ID mismatch.");
                }
                else if (!await VenueExists(id))
                {
                    return NotFound($"The Venue ID specified does not exist. Venue ID: {id}");
                }
                else if (!venue.IsActive)
                {
                    return NotFound($"No Active Venue found for ID: {id}");
                }


                var eventTypeExists = await context.EventTypes.AnyAsync(e => e.EventTypeId == venue.EventTypeId && e.IsActive);

                if (!eventTypeExists)
                {
                    return BadRequest($"The specified event type with ID ({venue.EventTypeId}) does not exist.");
                }


                context.Entry(venue).State = EntityState.Modified;
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
        [Route("DeleteVenue")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            try
            {
                var venue = await context.Venues.FindAsync(id);
                bool hasBookings = await context.Bookings.AnyAsync(b => b.VenueId == id);

                if (!await VenueExists(id) || venue == null)
                {
                    return NotFound($"The Venue ID specified does not exist. Venue ID: {id}");
                }
                else if (!venue.IsActive)
                {
                    return NotFound($"No Active Venues found for ID: {id}");
                }
                else if (hasBookings)
                {
                    return BadRequest("Cannot delete venue with existing bookings.");
                }

                venue.IsActive = false;
                context.Update(venue);
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

        private async Task<bool> VenueExists(int id)
        {
            return await context.Venues.AnyAsync(v => v.VenueId == id);
        }
    }
}
