namespace EventEaseBooking.Models;

public partial class Admin : BaseModel
{
    public int AdminId { get; set; }

    public string AdminName { get; set; } = null!;

    public string AdminEmail { get; set; } = null!;

    public string AdminRole { get; set; } = null!;

    public string AdminPosition { get; set; } = null!;

    public string? Action { get; set; }
}
