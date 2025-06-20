using AutoMapper;
using EventEaseAPI.Data.ApplicationDbContext;
using EventEaseAPI.DTOs.Booking;
using EventEaseAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingReadDto>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings
                .Where(b => b.IsActive) // Only active bookings
                .Include(b => b.Client)
                .Include(b => b.Event)
                    .ThenInclude(e => e.Venue)
                .Include(b => b.Event)
                    .ThenInclude(e => e.EventType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookingReadDto>>(bookings);
        }

        public async Task<BookingReadDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings
                .Where(b => b.IsActive) // Only active bookings
                .Include(b => b.Client)
                .Include(b => b.Event)
                    .ThenInclude(e => e.Venue)
                .Include(b => b.Event)
                    .ThenInclude(e => e.EventType)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            return booking == null ? null : _mapper.Map<BookingReadDto>(booking);
        }

        public async Task<BookingReadDto> CreateBookingAsync(BookingCreateDto dto)
        {
            // Get the event with its venue
            var targetEvent = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.EventId == dto.EventId);

            if (targetEvent == null)
                throw new ArgumentException("Invalid EventId provided.");

            // Check for conflict: same date, time, and venue with active bookings only
            bool isConflict = await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.IsActive)
                .AnyAsync(b =>
                    b.BookingDate == dto.BookingDate &&
                    b.BookingTime == dto.BookingTime &&
                    b.Event != null &&
                    b.Event.VenueId == targetEvent.VenueId
                );

            if (isConflict)
                throw new InvalidOperationException("This venue is already booked at the selected date and time.");

            // Map the DTO to the Booking entity
            var booking = _mapper.Map<Booking>(dto);
            booking.IsActive = true; // Mark new booking as active
            await _context.Bookings.AddAsync(booking);

            // Mark venue as booked (inactive)
            if (targetEvent.Venue != null)
            {
                targetEvent.Venue.IsActive = false;
                _context.Venues.Update(targetEvent.Venue);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Could not save booking due to a database error.", ex);
            }

            // Return the created booking as a read DTO
            var createdBooking = await GetBookingByIdAsync(booking.BookingId);
            if (createdBooking == null)
                throw new InvalidOperationException("Failed to load the created booking.");

            return createdBooking;
        }

        public async Task<bool> UpdateBookingAsync(int id, BookingUpdateDto dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null || !booking.IsActive)
                return false;

            _mapper.Map(dto, booking);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Failed to update booking due to concurrency issue.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Database error while updating the booking.", ex);
            }
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Event)
                .ThenInclude(e => e.Venue)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null || !booking.IsActive)
                return false;

            booking.IsActive = false;

            if (booking.Event?.Venue != null)
            {
                booking.Event.Venue.IsActive = true; // Mark venue available again
                _context.Venues.Update(booking.Event.Venue);
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to cancel booking due to database error.", ex);
            }
        }

        public async Task ExpirePastBookingsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var expiredBookings = await _context.Bookings
                .Include(b => b.Event)
                .ThenInclude(e => e.Venue)
                .Where(b => b.IsActive &&
                            b.Event != null &&
                            b.Event.EventDate < today)
                .ToListAsync();

            foreach (var booking in expiredBookings)
            {
                booking.IsActive = false;
                if (booking.Event.Venue != null)
                {
                    booking.Event.Venue.IsActive = true; // Mark venue available
                    _context.Venues.Update(booking.Event.Venue);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
