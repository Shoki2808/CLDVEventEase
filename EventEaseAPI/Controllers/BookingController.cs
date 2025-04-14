using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BookingController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            try
            {
                var Bookings = await context.Bookings.Where(b => b.IsActive).ToListAsync();
                if (Bookings.Count == 0)
                {
                    return NotFound("No active Bookings found.");
                }

                return Ok(Bookings);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching Bookings.\n{e.Message}");
            }

        }


        [HttpGet]
        [Route("GetBookingById")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            try
            {
                var booking = await context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);

                if (!await BookingExists(id))
                {
                    return NotFound($"The Booking with Booking ID {id} does not exist");
                }
                else if (!booking.IsActive)
                {
                    return NotFound($"No active Bookings found for Booking ID {id}");
                }


                return Ok(booking);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching the Booking specified. Booking ID {id}\n{e.Message}");
            }

        }

        [HttpPost]
        [Route("CreateBooking")]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] Booking booking)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var venueExists = await context.Venues.AnyAsync(v => v.VenueId == booking.VenueId && v.IsActive);
                var eventExists = await context.Events.AnyAsync(e => e.EventId == booking.EventId && e.IsActive);
                var clientExists = await context.Clients.AnyAsync(c => c.ClientId == booking.ClientId && c.IsActive);

                //if (!venueExists && !eventExists && !clientExists)
                //{
                //    return BadRequest($"The Booking ID ({booking.EventId}), the Venue ID ({booking.VenueId}) and The Client ID {booking.ClientId} specified do not exist.");
                //}
                //else if (!venueExists)
                //{
                //    return BadRequest($"The specified venue with ID ({booking.VenueId}) does not exist.");
                //}
                //else if (!eventExists)
                //{
                //    return BadRequest($"The specified Event with ID ({booking.EventId}) does not exist.");
                //}
                //else if (!clientExists)
                //{
                //    return BadRequest($"The specified Client with ID ({booking.ClientId}) does not exist.");
                //}

                booking.IsActive = true;
                context.Bookings.Add(booking);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
            }
            catch (DbUpdateException dbue)
            {
                // Logs can be added here
                return StatusCode(500, $"An error occurred while saving the Booking to the database.\n{dbue.Message}");
            }
            catch (Exception e)
            {
                // Logs can be added here
                return StatusCode(500, $"An unexpected error occurred. Please try again later.\n{e.Message}");
            }
        }



        [HttpPut]
        [Route("UpdateBooking")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            try
            {

                if (id != booking.BookingId)
                {
                    return BadRequest("Booking ID mismatch.");
                }
                else if (!await BookingExists(id))
                {
                    return NotFound($"The Booking ID specified does not exist. Booking ID: {id}");
                }
                else if (!booking.IsActive)
                {
                    return NotFound($"No Active Bookings found for ID: {id}");
                }

                var venueExists = await context.Venues.AnyAsync(v => v.VenueId == booking.VenueId && v.IsActive);
                var eventExists = await context.Events.AnyAsync(e => e.EventId == booking.EventId && e.IsActive);
                var clientExists = await context.Clients.AnyAsync(c => c.ClientId == booking.ClientId && c.IsActive);

                if (!venueExists && !eventExists && !clientExists)
                {
                    return BadRequest($"The Booking ID ({booking.EventId}), the Venue ID ({booking.VenueId}) and The Client ID {booking.ClientId} specified do not exist.");
                }
                else if (!venueExists)
                {
                    return BadRequest($"The specified venue with ID ({booking.VenueId}) does not exist.");
                }
                else if (!eventExists)
                {
                    return BadRequest($"The specified Event with ID ({booking.EventId}) does not exist.");
                }
                else if (!clientExists)
                {
                    return BadRequest($"The specified Client with ID ({booking.ClientId}) does not exist.");
                }


                context.Entry(booking).State = EntityState.Modified;
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
        [HttpPut]
        [Route("DeleteBooking")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var booking = await context.Bookings.FindAsync(id);
                //bool hasBookings = await context.Bookings.AnyAsync(b => b.BookingId == id);

                if (!await BookingExists(id) || booking == null)
                {
                    return NotFound($"The Booking ID specified does not exist. Booking ID: {id}");
                }
                else if (!booking.IsActive)
                {
                    return NotFound($"No Active Bookings found for ID: {id}");
                }
                

                booking.IsActive = false;
                context.Update(booking);
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
        //PUT https://localhost:7109/api/Booking/DeleteBooking?id=13 HTTP/2

        private async Task<bool> BookingExists(int id)
        {
            var booking = await context.Bookings.AnyAsync(b => b.BookingId == id);
            return booking;
        }
    }
}
