using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class Client : BaseModel
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [StringLength(100)]
        public string ClientName { get; set; } = null!;

        [Required]
        public string ContactDetails { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string ClientEmail { get; set; } = null!;

        //[Required]
        //public string Password { get; set; } = null!;

        //[Required]
        //public string EventName { get; set; } = null!;

        //public int? BookingId { get; set; }

        //public int? EventId { get; set; }

        //public int? VenueId { get; set; }

        //[JsonIgnore]
        //[ForeignKey("BookingId")]
        //public virtual Booking? Booking { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        //[JsonIgnore]
        //[ForeignKey("EventId")]
        //public virtual Event? Event { get; set; }

        //[JsonIgnore]
        //[ForeignKey("VenueId")]
        //public virtual Venue? Venue { get; set; }
    }
}
