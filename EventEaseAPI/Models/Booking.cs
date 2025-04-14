

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class Booking : BaseModel
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public DateOnly BookingDate { get; set; }

        [Required]
        public TimeOnly BookingTime { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int VenueId { get; set; }

        public int? ClientId { get; set; }
        [JsonIgnore]
        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }

        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

        [JsonIgnore]
        [ForeignKey("EventId")]
        public virtual Event? Event { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [JsonIgnore]
        [ForeignKey("VenueId")]
        public virtual Venue? Venue { get; set; } = null!;
    }
}
