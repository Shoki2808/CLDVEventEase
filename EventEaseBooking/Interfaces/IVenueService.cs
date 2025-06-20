using EventEaseBooking.Models;

namespace EventEaseBooking.Interfaces
{
    public interface IVenueService
    {
        public Task<Venue> AddVenue(Venue venueModel);
        public Task<Venue> UpdateVenue(Venue venueModel);

        public Task<Venue> GetVenueById(int id);    
        public Task<List<Venue>> GetVenues();
        Task<bool> GetVenueAvailability(int id, DateOnly startDate, DateOnly endDate);
    }
}
