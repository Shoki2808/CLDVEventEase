using EventEaseAPI.DTOs.Event;
using EventEaseAPI.Enums;

namespace EventEaseAPI.Interfaces
{
    public interface IEventService
    {
       
        Task<IEnumerable<EventReadDto>> GetAllAsync();
        Task<EventReadDto> GetByIdAsync(int id);

        Task<(CreateEventResult Result, EventReadDto? Data)> CreateAsync(EventCreateDto dto);
        Task<bool> UpdateAsync(int id, EventUpdateDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
