
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

        public string? EventDescription { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public int EventTypeId { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        //[JsonIgnore]
        //public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
        //[JsonIgnore]
        //[ForeignKey("EventTypeId")]
        //public virtual EventType? EventType { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<EventVendor> EventVendors { get; set; } = new List<EventVendor>();
        //[JsonIgnore]
        //[ForeignKey("VenueId")]
        //public virtual Venue? Venue { get; set; } = null!;
    }
}
