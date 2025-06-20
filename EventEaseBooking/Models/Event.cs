namespace EventEaseBooking.Models;

public partial class Event : BaseModel
{
    public int EventId { get; set; } = 0;

    public string EventName { get; set; } = null!;

    public DateOnly EventDate { get; set; }

    public string? EventDescription { get; set; }

    public int VenueId { get; set; }

    public int EventTypeId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual EventType EventType { get; set; } = null!;

    public virtual ICollection<EventVendor> EventVendors { get; set; } = new List<EventVendor>();

    public virtual Venue Venue { get; set; } = null!;

    public TimeOnly StartTime { get; set; } = new TimeOnly();

    public TimeOnly EndTime { get; set; } = new TimeOnly();
    public string VenueName { get; set; }
    public string EventTypeName { get; set; }
    public bool IsActive { get; set; } = true;

    public string Status { get; set; } = "Inactive";

}
