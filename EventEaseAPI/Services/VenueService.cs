using AutoMapper;
using EventEaseAPI.DTOs.Booking;
using Microsoft.EntityFrameworkCore;
using EventEaseAPI.Interfaces;
using EventEaseAPI.Enums;
using EventEaseAPI.DTOs.Venue;
using EventEaseAPI.Data.ApplicationDbContext;

namespace EventEaseAPI.Services
{
    public class VenueService : IVenueService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VenueService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VenueReadDto>> GetAllAsync()
        {
            var venues = await _context.Venues
                .Include(v => v.Events)
                .ToListAsync();

            return _mapper.Map<IEnumerable<VenueReadDto>>(venues);
        }

        public async Task<VenueReadDto?> GetByIdAsync(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Events)
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) return null;

            return _mapper.Map<VenueReadDto>(venue);
        }

        public async Task<VenueReadDto> CreateAsync(VenueCreateDto dto, string imageUrl)
        {
            var venue = _mapper.Map<Venue>(dto);
            venue.ImageUrl = imageUrl;

            try
            {
                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to create venue due to database error.", ex);
            }

            return _mapper.Map<VenueReadDto>(venue);
        }
        public async Task<bool> VenueAvailability(int id, DateOnly startDate, DateOnly endDate)
        {
            bool result = false;
            try
            {
    bool isConflict = await _context.Events.AnyAsync(e =>
                e.VenueId == id &&
                e.EventDate == startDate &&
                e.EventDate == endDate); 

            if (isConflict)
            {
                result = true;
            }
                return result;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Failed to validate availability", ex);
            }
       


        }

        public async Task<bool> UpdateAsync(int id, VenueUpdateDto dto, string imageUrl)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return false;

            _mapper.Map(dto, venue);

            if (!string.IsNullOrEmpty(imageUrl))
            {
                venue.ImageUrl = imageUrl;
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Failed to update venue due to concurrency conflict.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Database error while updating the venue.", ex);
            }
        }

        public async Task<DeleteVenueResult> DeleteAsync(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Events)
                    .ThenInclude(e => e.Bookings)
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null)
                return DeleteVenueResult.NotFound;

            bool hasBookings = venue.Events.Any(e => e.Bookings.Any());
            if (hasBookings)
                return DeleteVenueResult.HasBookings;

            _context.Venues.Remove(venue);

            try
            {
                await _context.SaveChangesAsync();
                return DeleteVenueResult.Success;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to delete venue due to database error.", ex);
            }
        }
    }
}
