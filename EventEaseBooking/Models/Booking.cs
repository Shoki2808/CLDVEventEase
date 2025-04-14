namespace EventEaseBooking.Models;

public partial class Booking : BaseModel
{
    public int BookingId { get; set; }

    public DateOnly BookingDate { get; set; }

    public TimeOnly BookingTime { get; set; }

    public int EventId { get; set; }

    public int VenueId { get; set; }

    public int ClientId { get; set; }

    public virtual Client? Client { get; set; }


    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Venue Venue { get; set; } = null!;
}
