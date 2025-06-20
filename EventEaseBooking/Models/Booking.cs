using System.ComponentModel.DataAnnotations;

namespace EventEaseBooking.Models;

public partial class Booking : BaseModel
{
    //public int BookingId { get; set; }

    ////public DateOnly BookingDate { get; set; }
    //public DateTime BookingDate { get; set; }


    //public TimeOnly BookingTime { get; set; }

    //public int EventId { get; set; }

    //public int VenueId { get; set; }

    //public int ClientId { get; set; }



    //public virtual Client? Client { get; set; }


    //public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    //public virtual Event Event { get; set; } = null!;

    //public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    //public virtual Venue Venue { get; set; } = null!;

    //public string Status { get; set; } = "Confirmed"; // e.g., Confirmed, Cancelled, Pending





    public int BookingId { get; set; }

    //[Required(ErrorMessage = "Booking date is required.")]

    public DateOnly BookingDate { get; set; }

    public TimeOnly BookingTime { get; set; }

    //public int EventId { get; set; }

    public string? EventName { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string EventTypeName { get; set; }

    //public int? ClientId { get; set; }

    public string? ClientName { get; set; }

    //public bool IsActive { get; set; }
    public string? VenueName { get; set; }

    public string AvailabilityStatus { get; set; } = "Inactive";


    public int EventId { get; set; }

    public int? ClientId { get; set; }
}
