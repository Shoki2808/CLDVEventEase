namespace EventEaseBooking.Models;

public partial class Payment : BaseModel
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Action { get; set; }

    public int? BookingId { get; set; }

    public virtual Booking? Booking { get; set; }
}
