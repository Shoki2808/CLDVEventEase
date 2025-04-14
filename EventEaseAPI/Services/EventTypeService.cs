
namespace EventEaseAPI.Services
{
    public class EventTypeService : GenericService<EventType>
    {
        public EventTypeService(ApplicationDbContext context) : base(context) { }
    }
}
