namespace EventEaseBooking.Models;

public partial class EventType : BaseModel
{
    public int EventTypeId { get; set; }

    public string EventTypeName { get; set; }

    //public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    //public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
}
