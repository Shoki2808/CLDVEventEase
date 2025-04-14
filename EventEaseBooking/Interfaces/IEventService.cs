
using EventEaseBooking.Models;

namespace EventEaseBooking.Interfaces
{
    public interface IEventService
    {
        public Task<List<EventType>> GetEventTypes();
        public Task<List<Event>> GetEvents();
        public Task<Event> GetEventById(int eventId);

        public Task<Event> AddEvent(Event eventModel);

    }
}
