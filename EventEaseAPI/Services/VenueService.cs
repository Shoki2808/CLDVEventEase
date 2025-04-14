

namespace EventEaseAPI.Services
{
    public class VenueService : GenericService<Venue>
    {
        public VenueService(ApplicationDbContext context) : base(context) { }
    }
}
