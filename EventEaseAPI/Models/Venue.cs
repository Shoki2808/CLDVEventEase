using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class Venue : BaseModel
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Venue name cannot exceed 100 characters.")]
        public string VenueName { get; set; } = null!;

        [Required]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string Location { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int EventTypeId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
        [JsonIgnore]
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
