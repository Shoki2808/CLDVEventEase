using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class Venue : BaseModel
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string VenueName { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }
        

        [JsonIgnore]
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
