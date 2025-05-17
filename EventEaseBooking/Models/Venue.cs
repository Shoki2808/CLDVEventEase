namespace EventEaseBooking.Models;

public partial class Venue : BaseModel
{
    public int VenueId { get; set; } = 0;

    public string VenueName { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int Capacity { get; set; } = 0;

    public string? ImageUrl { get; set; } = string.Empty;

    public int EventTypeId { get; set; } = 0;

    //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    //public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    //public virtual ICollection<Event> Events { get; set; } = new List<Event>();


}
