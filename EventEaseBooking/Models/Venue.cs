namespace EventEaseBooking.Models;

public partial class Venue : BaseModel
{
    public int VenueId { get; set; }

    public string VenueName { get; set; } 

    public string Location { get; set; } 

    public int Capacity { get; set; }

    public string? ImageUrl { get; set; }

    public int EventTypeId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();


}
