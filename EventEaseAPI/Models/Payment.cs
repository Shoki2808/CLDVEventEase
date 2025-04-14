using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class Payment : BaseModel
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public DateOnly PaymentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = null!;

        public string? Action { get; set; }

        public int? BookingId { get; set; }

        [ForeignKey("BookingId")]
        [JsonIgnore]
        public virtual Booking? Booking { get; set; }
    }
}
