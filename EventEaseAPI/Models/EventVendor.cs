using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class EventVendor : BaseModel
    {
        [Key]
        public int EventVendorId { get; set; }

        [Required]
        [StringLength(100)]
        public string VendorName { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string ServiceOffering { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string ContactDetails { get; set; } = null!;

        [Required]


        public int EventId { get; set; }

        [JsonIgnore]
        [ForeignKey("EventId")]

        public virtual Event? Event { get; set; } = null!;
    }
}
