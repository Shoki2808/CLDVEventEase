using AutoMapper;
using EventEaseAPI.Data.ApplicationDbContext;
using EventEaseAPI.DTOs.Event;
using EventEaseAPI.Enums;
using EventEaseAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EventService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventReadDto>> GetAllAsync()
        {
            var events = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }

        public async Task<EventReadDto?> GetByIdAsync(int id)
        {
            var ev = await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .FirstOrDefaultAsync(e => e.EventId == id);

            return ev == null ? null : _mapper.Map<EventReadDto>(ev);
        }

        public async Task<(CreateEventResult Result, EventReadDto? Data)> CreateAsync(EventCreateDto dto)
        {
            var venue = await _context.Venues.FirstOrDefaultAsync(v => v.VenueId == dto.VenueId);

            if (venue == null)
                return (CreateEventResult.VenueNotFound, null);

            
            if (!venue.IsActive)
                return (CreateEventResult.VenueNotActive, null);

            
            bool isConflict = await _context.Events.AnyAsync(e =>
                e.VenueId == dto.VenueId &&
                e.EventDate == dto.EventDate &&
                (
                (dto.StartTime >= e.StartTime && dto.EndTime <= e.EndTime)
                    //(dto.StartTime >= e.StartTime && dto.StartTime < e.EndTime) ||
                    //(dto.EndTime > e.StartTime && dto.EndTime <= e.EndTime) ||
                    //(dto.StartTime <= e.StartTime && dto.EndTime >= e.EndTime)
                ));

            if (isConflict)
                return (CreateEventResult.VenueAlreadyBooked, null);

            
            var ev = _mapper.Map<Event>(dto);

            try
            {
                _context.Events.Add(ev);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to create event due to a database error.", ex);
            }

            var readDto = _mapper.Map<EventReadDto>(ev);
            return (CreateEventResult.Success, readDto);
        }


        public async Task<bool> UpdateAsync(int id, EventUpdateDto dto)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return false;

            _mapper.Map(dto, ev);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Failed to update event due to concurrency issue.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Database error while updating the event.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return false;

            _context.Events.Remove(ev);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Could not delete the event due to a database error.", ex);
            }
        }
    }
}
