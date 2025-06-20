using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class AdminController : ControllerBase
    {
       /*** private readonly ApplicationDbContext context;


        public AdminController(ApplicationDbContext context)
        {
            this.context = context;

        }

        [HttpGet]
        [Route("GetAllAdmins")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            try
            {
                var admins = await context.Admins.Where(a => a.IsActive).ToListAsync();
                if (admins.Count == 0)
                {
                    return NotFound("No active Admins found.");
                }

                return Ok(admins);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching Admins.\n{e.Message}");
            }

        }


        [HttpGet]
        [Route("GetAdminById")]
        public async Task<ActionResult<Admin>> GetAdminById(int id)
        {
            try
            {
                var admin = await context.Admins.FirstOrDefaultAsync(a => a.AdminId == id);

                if (!await AdminExists(id))
                {
                    return NotFound($"The Admin with Admin ID {id} does not exist");
                }
                else if (!admin.IsActive)
                {
                    return NotFound($"No active Admins found for venue ID {id}");
                }


                return Ok(admin);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while fetching the Admin specified. Admin ID {id}\n{e.Message}");
            }

        }


        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<ActionResult<Venue>> CreateAdmin([FromBody] Admin admin)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                admin.IsActive = true;
                context.Admins.Add(admin);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAdminById), new { id = admin.AdminId }, admin);
            }
            catch (DbUpdateException dbue)
            {

                return StatusCode(500, $"An error occurred while saving the Admin to the database.\n{dbue.Message}");
            }
            catch (Exception e)
            {

                return StatusCode(500, $"An unexpected error occurred. Please try again later.\n{e.Message}");
            }
        }


        [HttpPut]
        [Route("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] Admin admin)
        {
            try
            {

                if (id != admin.AdminId)
                {
                    return BadRequest("Admin ID mismatch.");
                }
                else if (!await AdminExists(id))
                {
                    return NotFound($"The Admin ID specified does not exist. Admin ID: {id}");
                }
                else if (!admin.IsActive)
                {
                    return NotFound($"No Active Admins found for ID: {id}");
                }


                context.Entry(admin).State = EntityState.Modified;
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
        [Route("DeleteAdmin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                var admin = await context.Admins.FindAsync(id);

                if (!await AdminExists(id) || admin == null)
                {
                    return NotFound($"The Admin ID specified does not exist. Admin ID: {id}");
                }
                else if (!admin.IsActive)
                {
                    return NotFound($"No Active Admins found for ID: {id}");
                }

                admin.IsActive = false;
                context.Update(admin);
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

        private async Task<bool> AdminExists(int id)
        {
            return await context.Admins.AnyAsync(a => a.AdminId == id);
        }

        /*************************************************************Venue Management***********************************************************************/

        /**
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
                if (!eventTypeExists)
                {
                    return BadRequest($"The specified event type with ID ({venue.EventTypeId}) does not exist.");
                }

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
                    return BadRequest("Event ID mismatch.");
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


        /********************************************************************************************Event Management**********************************************************************/
        /**

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
        public async Task<ActionResult<Event>> CreateEvent([FromBody] Event eventItem)
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
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event eventItem)
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
        
        /*************************************************************************************Booking Management***************************************************************************/

        /**
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
        [HttpDelete]
        [Route("DeleteBooking")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var booking = await context.Bookings.FindAsync(id);
                bool hasBookings = await context.Bookings.AnyAsync(b => b.BookingId == id);

                if (!await BookingExists(id) || booking == null)
                {
                    return NotFound($"The Booking ID specified does not exist. Booking ID: {id}");
                }
                else if (!booking.IsActive)
                {
                    return NotFound($"No Active Bookings found for ID: {id}");
                }
                else if (hasBookings)
                {
                    return BadRequest("Cannot delete Booking with existing bookings.");
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


        private async Task<bool> BookingExists(int id)
        {
            var booking = await context.Bookings.AnyAsync(b => b.BookingId == id);
            return booking;
        } **/


        /**********************************************************************************Client Management**********************************************************************/

        /**
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
                //if (!ModelState.IsValid) return BadRequest(ModelState);

                //var bookingExists = await context.Bookings.AnyAsync(b => b.BookingId == client.BookingId);

        
                //else if (!bookingExists)
                //{
                //    return BadRequest($"The specified Booking with ID ({client.BookingId}) does not exist.");
                //}

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
                //var venueExists = await context.Venues.AnyAsync(v => v.VenueId == client.VenueId && v.IsActive);
                //var eventExists = await context.Events.AnyAsync(e => e.EventId == client.EventId && e.IsActive);
                //var bookingExists = await context.Bookings.AnyAsync(b => b.BookingId == client.BookingId);

                //if (!venueExists && !eventExists && !bookingExists)
                //{
                //    return BadRequest($"The Event ID ({client.EventId}), the Venue ID ({client.VenueId}) and The Client ID {client.BookingId} specified do not exist.");
                //}
                //else if (!venueExists)
                //{
                //    return BadRequest($"The specified venue with ID ({client.VenueId}) does not exist.");
                //}
                //else if (!eventExists)
                //{
                //    return BadRequest($"The specified Event with ID ({client.EventId}) does not exist.");
                //}
                //else if (!bookingExists)
                //{
                //    return BadRequest($"The specified Booking with ID ({client.BookingId}) does not exist.");
                //}


                context.Entry(client).State = EntityState.Modified;
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

        /************************************************************************Event Type Management******************************************************************/

        /**
        // GET: api/Get All Event Types
        [HttpGet]
        [Route("GetAllEventTypes")]
        public async Task<IActionResult> GetAllEventTypes()
        {
            var eventTypes = await context.EventTypes.Where(e => e.IsActive).ToListAsync();
            return Ok(eventTypes);
        }

        // GET: api/Get Event Type By Id
        [HttpGet]
        [Route("GetEventTypeById")]
        public async Task<IActionResult> GetEventTypeById(int id)
        {
            var eventType = await context.EventTypes.FirstOrDefaultAsync(e => e.EventTypeId == id && e.IsActive);
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
            context.EventTypes.Add(eventType);
            await context.SaveChangesAsync();
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

            var existingEventType = await context.EventTypes.FindAsync(id);
            if (existingEventType == null || !existingEventType.IsActive)
            {
                return NotFound();
            }

            existingEventType.EventTypeName = eventType.EventTypeName;
            context.EventTypes.Update(existingEventType);
            await context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Delete EventType (Soft Delete)
        [HttpDelete]
        [Route("DeleteEventType")]
        public async Task<IActionResult> DeleteEventType(int id)
        {
            var eventType = await context.EventTypes.FindAsync(id);
            if (eventType == null || !eventType.IsActive)
            {
                return NotFound();
            }

            eventType.IsActive = false; // Soft delete
            context.EventTypes.Update(eventType);
            await context.SaveChangesAsync();

            return NoContent();
        }

        **/
    }
}









