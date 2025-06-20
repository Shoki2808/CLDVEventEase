using Microsoft.AspNetCore.Http;

namespace EventEaseAPI.DTOs.Venue
{
    public class VenueUpdateDto
    {
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int? Capacity { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool? IsActive { get; set; }
    }
}
