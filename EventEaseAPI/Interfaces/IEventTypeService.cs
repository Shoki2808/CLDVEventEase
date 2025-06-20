using EventEaseAPI.DTOs.EventType;

namespace EventEaseAPI.Interfaces
{
    public interface IEventTypeService
    {
       
            Task<IEnumerable<EventTypeReadDto>> GetAllAsync();
            Task<EventTypeReadDto> GetByIdAsync(int id);
            Task<EventTypeReadDto> CreateAsync(EventTypeCreateDto dto);
            Task<bool> UpdateAsync(int id, EventTypeUpdateDto dto);
            Task<bool> DeleteAsync(int id);

    }
}
