using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EventEaseAPI.Interfaces;
using EventEaseAPI.DTOs.EventType;
using EventEaseAPI.Data.ApplicationDbContext;

namespace EventEaseAPI.Services
{
    public class EventTypeService : IEventTypeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EventTypeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventTypeReadDto>> GetAllAsync()
        {
            var types = await _context.EventTypes.ToListAsync();
            return _mapper.Map<IEnumerable<EventTypeReadDto>>(types);
        }

        public async Task<EventTypeReadDto?> GetByIdAsync(int id)
        {
            var type = await _context.EventTypes.FindAsync(id);
            return type == null ? null : _mapper.Map<EventTypeReadDto>(type);
        }

        public async Task<EventTypeReadDto> CreateAsync(EventTypeCreateDto dto)
        {
            var type = _mapper.Map<EventType>(dto);

            try
            {
                _context.EventTypes.Add(type);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to create event type due to database error.", ex);
            }

            return _mapper.Map<EventTypeReadDto>(type);
        }

        public async Task<bool> UpdateAsync(int id, EventTypeUpdateDto dto)
        {
            var type = await _context.EventTypes.FindAsync(id);
            if (type == null) return false;

            _mapper.Map(dto, type);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Failed to update event type due to concurrency conflict.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Database error while updating the event type.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var type = await _context.EventTypes.FindAsync(id);
            if (type == null) return false;

            _context.EventTypes.Remove(type);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to delete event type due to database error.", ex);
            }
        }
    }
}
