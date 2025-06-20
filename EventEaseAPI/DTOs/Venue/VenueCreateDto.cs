using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventEaseAPI.DTOs.Venue
{
    public class VenueCreateDto
    {
        [Required]
        public string VenueName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Capacity { get; set; }

        public IFormFile ImageFile { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
