using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Services
{
    public class EventVendorService
    {
        private readonly ApplicationDbContext context;

        public EventVendorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<EventVendor>> GetAllEventVendorsAsync()
        {
            return await context.EventVendors.Where(e => e.IsActive).ToListAsync();
        }

        public async Task<EventVendor?> GetEventVendorByIdAsync(int id)
        {
            return await context.EventVendors.FirstOrDefaultAsync(e => e.EventVendorId == id);
        }

        public async Task<bool> EventVendorExistsAsync(int id)
        {
            return await context.EventVendors.AnyAsync(v => v.EventVendorId == id);
        }

        public async Task<EventVendor?> CreateEventVendorAsync(EventVendor eventVendor)
        {
            var eventExists = await context.Events.AnyAsync(e => e.EventId == eventVendor.EventId && e.IsActive);
            if (!eventExists)
            {
                return null;
            }

            eventVendor.IsActive = true;
            context.EventVendors.Add(eventVendor);
            await context.SaveChangesAsync();
            return eventVendor;
        }

        public async Task<bool> UpdateEventVendorAsync(int id, EventVendor eventVendor)
        {
            if (id != eventVendor.EventVendorId || !await EventVendorExistsAsync(id))
            {
                return false;
            }

            context.Entry(eventVendor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventVendorAsync(int id)
        {
            var eventVendor = await context.EventVendors.FindAsync(id);
            if (eventVendor == null || !eventVendor.IsActive)
            {
                return false;
            }

            eventVendor.IsActive = false;
            context.Update(eventVendor);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
