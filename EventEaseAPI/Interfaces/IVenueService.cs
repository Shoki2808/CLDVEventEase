using EventEaseAPI.DTOs.Venue;
using EventEaseAPI.Enums;

namespace EventEaseAPI.Interfaces
{
    public interface IVenueService
    {
        Task<IEnumerable<VenueReadDto>> GetAllAsync();
        Task<VenueReadDto> GetByIdAsync(int id);
        Task<VenueReadDto> CreateAsync(VenueCreateDto dto, string imageUrl);
        Task<bool> UpdateAsync(int id, VenueUpdateDto dto, string imageUrl);
        Task<DeleteVenueResult> DeleteAsync(int id);
        Task<bool> VenueAvailability(int id, DateOnly startDate, DateOnly endDate);
    }
}
