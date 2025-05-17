using EventEaseBooking.Models;

namespace EventEaseBooking.Components.Pages
{
    public class GlobalParam
    {
        public Booking? bookingModel { get; set; }
        public Models.Event? eventModel { get; set; }
        public int BookingId { get; set; }

    }
}
