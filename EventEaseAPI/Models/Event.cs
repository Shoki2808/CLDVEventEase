using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class Event : BaseModel
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(200)]
        public string EventName { get; set; } = null!;

        [Required]
        public DateOnly EventDate { get; set; }
        [Required]

        public TimeOnly StartTime { get; set; } = new TimeOnly();
        [Required]

        public TimeOnly EndTime { get; set; } = new TimeOnly();

        public string? EventDescription { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public int EventTypeId { get; set; }

        
        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        
        [JsonIgnore]
        [ForeignKey("EventTypeId")]
        public virtual EventType? EventType { get; set; }

        
        [JsonIgnore]
        [ForeignKey("VenueId")]
        public virtual Venue? Venue { get; set; }

        
    }
}
