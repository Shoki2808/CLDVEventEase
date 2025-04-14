
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ClientController(ApplicationDbContext context)
        {
            this.context = context;
        }



        // GET: Client/Get all clients
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            var client = await context.Clients.Where(c => c.IsActive).ToListAsync();

            if (client.Count == 0)
            {
                return NotFound();
            }
            return Ok(client);
        }


        [HttpGet]
        [Route("GetClientById")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            try
            {
                var client = await context.Clients.FirstOrDefaultAsync(e => e.ClientId == id);

                if (!await ClientExists(id))
                {
                    return NotFound($"The Client with Client ID {id} does not exist");
                }
                else if (!client.IsActive)
                {
                    return NotFound($"No active clients found for Client ID {id}");
                }


                return Ok(client);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching the event specified. Client ID {id}\n{e.Message}");
            }

        }

        // POST: Client/Create Client
        [HttpPost]
        [Route("CreateClient")]
        public async Task<ActionResult<Client>> CreateClient([FromBody] Client client)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var venueExists = await context.Venues.AnyAsync(v => v.VenueId == client.VenueId && v.IsActive);
                var eventExists = await context.Events.AnyAsync(e => e.EventId == client.EventId && e.IsActive);
                var bookingExists = await context.Bookings.AnyAsync(b => b.BookingId == client.BookingId);

                if (!venueExists && !eventExists && !bookingExists)
                {
                    return BadRequest($"The Event ID ({client.EventId}), the Venue ID ({client.VenueId}) and The Client ID {client.BookingId} specified do not exist.");
                }
                else if (!venueExists)
                {
                    return BadRequest($"The specified venue with ID ({client.VenueId}) does not exist.");
                }
                else if (!eventExists)
                {
                    return BadRequest($"The specified Event with ID ({client.EventId}) does not exist.");
                }
                else if (!bookingExists)
                {
                    return BadRequest($"The specified Booking with ID ({client.BookingId}) does not exist.");
                }

                client.IsActive = true;
                context.Clients.Add(client);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetClientById), new { id = client.ClientId }, client);
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
        [Route("UpdateClient")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            try
            {

                if (id != client.ClientId)
                {
                    return BadRequest("ClientID mismatch.");
                }
                else if (!await ClientExists(id))
                {
                    return NotFound($"The Client ID specified does not exist. Client ID: {id}");
                }
                else if (!client.IsActive)
                {
                    return NotFound($"No Active Clients found for ID: {id}");
                }
                var venueExists = await context.Venues.AnyAsync(v => v.VenueId == client.VenueId && v.IsActive);
                var eventExists = await context.Events.AnyAsync(e => e.EventId == client.EventId && e.IsActive);
                var bookingExists = await context.Bookings.AnyAsync(b => b.BookingId == client.BookingId);

                if (!venueExists && !eventExists && !bookingExists)
                {
                    return BadRequest($"The Event ID ({client.EventId}), the Venue ID ({client.VenueId}) and The Client ID {client.BookingId} specified do not exist.");
                }
                else if (!venueExists)
                {
                    return BadRequest($"The specified venue with ID ({client.VenueId}) does not exist.");
                }
                else if (!eventExists)
                {
                    return BadRequest($"The specified Event with ID ({client.EventId}) does not exist.");
                }
                else if (!bookingExists)
                {
                    return BadRequest($"The specified Booking with ID ({client.BookingId}) does not exist.");
                }


                context.Entry(client).State = EntityState.Modified;
                await context.SaveChangesAsync();

                var updatedClient = await context.Clients.FindAsync(id);

                return Ok(updatedClient);

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



        // POST: Client/Delete
        [HttpDelete]
        [Route("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var client = await context.Clients.FindAsync(id);
                bool hasBookings = await context.Bookings.AnyAsync(b => b.ClientId == id);

                if (!await ClientExists(id) || client == null)
                {
                    return NotFound($"The Client ID specified does not exist. Client ID: {id}");
                }
                else if (!client.IsActive)
                {
                    return NotFound($"No Active Clients found for ID: {id}");
                }
                else if (hasBookings)
                {
                    return BadRequest("Cannot delete client with existing bookings.");
                }

                client.IsActive = false;
                context.Update(client);
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

        private async Task<bool> ClientExists(int id)
        {
            return await context.Clients.AnyAsync(e => e.ClientId == id);
        }

    }

}
