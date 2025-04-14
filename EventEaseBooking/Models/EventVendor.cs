namespace EventEaseBooking.Models;

public partial class EventVendor : BaseModel
{
    public int EventVendorId { get; set; }

    public string VendorName { get; set; } = null!;

    public string ServiceOffering { get; set; } = null!;

    public string ContactDetails { get; set; } = null!;

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;
}
