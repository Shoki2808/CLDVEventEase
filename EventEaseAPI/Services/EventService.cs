

namespace EventEaseAPI.Services
{
    public class EventService : GenericService<Event>
    {
        public EventService(ApplicationDbContext context) : base(context) { }
    }
}
