
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventEaseAPI.Models
{
    public partial class EventType : BaseModel
    {
        [Key]
        public int EventTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string EventTypeName { get; set; } = null!;

        //[JsonIgnore]
        //public virtual ICollection<Event> Events { get; set; } = new List<Event>();
        //[JsonIgnore]
        //public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
    }
}
