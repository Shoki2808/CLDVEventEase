using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    public int? ClientId { get; set; }

    [JsonIgnore]
    [ForeignKey("ClientId")]
    public virtual Client? Client { get; set; }

    [JsonIgnore]
    [ForeignKey("EventId")]
    public virtual Event? Event { get; set; }

}

