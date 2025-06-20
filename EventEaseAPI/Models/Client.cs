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


        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
