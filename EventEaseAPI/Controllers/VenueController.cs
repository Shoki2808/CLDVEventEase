using Microsoft.AspNetCore.Mvc;
using EventEaseAPI.Interfaces;
using EventEaseAPI.Enums;
using EventEaseAPI.DTOs.Venue;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;
        private readonly IBlobService _blobService;

        public VenueController(IVenueService venueService, IBlobService blobService)
        {
            _venueService = venueService;
            _blobService = blobService;
        }

        [HttpGet]
        [Route("GetAllVenues")]
        public async Task<IActionResult> GetAllVenues()
        {
            var venues = await _venueService.GetAllAsync();
            if (venues == null)
            {
                return NotFound("No active venues found.");
            }

            return Ok(venues);
        }

        [HttpGet]
        [Route("GetVenueById")]
        public async Task<IActionResult> GetVenueById(int id)
        {
            var venue = await _venueService.GetByIdAsync(id);
            if (venue == null)
                return NotFound($"No active venue found for ID {id}.");

            return Ok(venue);
        }

        [HttpGet]
        [Route("GetVenueStatus")]
        public async Task<bool> VenueAvailability(int id, DateOnly startDate, DateOnly endDate)
        {
            var venue = await _venueService.VenueAvailability(id, startDate, endDate);
            if (venue == true)
            {
                return true;
            }


            return venue;
        }

        [HttpPost]
        [Route("CreateVenue")]
        public async Task<IActionResult> CreateVenue([FromForm] VenueCreateDto dto)
        {
            string imageUrl = null;

            if (dto.ImageFile != null)
            {
                imageUrl = await _blobService.UploadAsync(dto.ImageFile, "venues");
            }

            var createdVenue = await _venueService.CreateAsync(dto, imageUrl);
            return CreatedAtAction(nameof(GetVenueById), new { id = createdVenue.VenueId }, createdVenue);
        }

        [HttpPut]
        [Route("UpdateVenue")]
        public async Task<IActionResult> UpdateVenue(int id, [FromForm] VenueUpdateDto dto)
        {
            var existingVenue = await _venueService.GetByIdAsync(id);
            if (existingVenue == null)
                return NotFound($"No active venue found for ID {id}.");

            string imageUrl = null;
            if (dto.ImageFile != null)
            {
                imageUrl = await _blobService.UploadAsync(dto.ImageFile, "venues");
            }

            var updated = await _venueService.UpdateAsync(id, dto, imageUrl);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete]
        [Route("DeleteVenue")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var result = await _venueService.DeleteAsync(id);

            return result switch
            {
                DeleteVenueResult.Success => NoContent(),
                DeleteVenueResult.NotFound => NotFound(),
                DeleteVenueResult.HasBookings => BadRequest("Cannot delete venue with existing bookings."),
                _ => StatusCode(500, "An unexpected error occurred.")
            };
        }
    }
}
