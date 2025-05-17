namespace EventEaseBooking.Models
{
    public class BookingViewModel
    {
        public Booking BookingModel { get; set; } = new Booking();
        public Event EventModel { get; set; } = new Event();
        public Venue VenueModel { get; set; } = new Venue();
        public Client Client { get; set; } = new();

    }
}
