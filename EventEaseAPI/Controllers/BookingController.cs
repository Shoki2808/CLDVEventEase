using Microsoft.AspNetCore.Mvc;
using EventEaseAPI.Interfaces;
using System.Net.Mime;
using EventEaseAPI.DTOs.Booking;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet]
        [Route("GetBookingById")]
        public async Task<ActionResult<BookingReadDto>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound(new { message = $"Booking with ID {id} not found." });
            return Ok(booking);
        }

        [HttpPost]
        [Route("CreateBooking")]
        public async Task<ActionResult<BookingReadDto>> CreateBooking([FromBody] BookingCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdBooking = await _bookingService.CreateBookingAsync(dto);
                return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.BookingId }, createdBooking);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception)
            {
                // Log exception here if desired
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPut]
        [Route("UpdateBooking")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _bookingService.UpdateBookingAsync(id, dto);
            if (!updated) return NotFound(new { message = $"Booking with ID {id} not found." });
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteBooking")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var deleted = await _bookingService.DeleteBookingAsync(id);
            if (!deleted) return NotFound(new { message = $"Booking with ID {id} not found." });
            return NoContent();
        }
    }
}
