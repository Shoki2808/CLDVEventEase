

namespace EventEaseAPI.Services
{
    public class BookingService : GenericService<Booking>
    {
        public BookingService(ApplicationDbContext context) : base(context) { }
    }
}
