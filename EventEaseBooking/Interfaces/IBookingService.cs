using EventEaseBooking.Models;

namespace EventEaseBooking.Interfaces
{
    public interface IBookingService
    {
        public  Task<List<Venue>> GetVenues();
        public Task<Booking> CreateBooking(Booking bookingModel);
        public Task<List<Booking>> GetBookings();
        public Task<bool> DeleteBooking(int bookingId);
        public Task<Booking> UpdateBooking(Booking bookingModel);
        public Task<Booking> GetBookingById(int bookingId);


    }
}
