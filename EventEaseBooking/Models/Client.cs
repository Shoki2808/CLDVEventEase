namespace EventEaseBooking.Models;

public partial class Client : BaseModel
{
    public int ClientId { get; set; } = 0;

    public string ClientName { get; set; } = null!;

    public string ContactDetails { get; set; } = null!;

    public string ClientEmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string EventName { get; set; } = "null"!;

    public int? BookingId { get; set; } = 1;

    public int? EventId { get; set; } = 1;

    public int? VenueId { get; set; } = 1;

    public bool IsActive { get; set; }
    //public virtual Booking? Booking { get; set; }

    //public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    //public virtual Event? Event { get; set; }

    //public virtual Venue? Venue { get; set; }
}
