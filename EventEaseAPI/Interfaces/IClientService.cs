using EventEaseAPI.DTOs.Client;

namespace EventEaseAPI.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientReadDto>> GetAllAsync();
        Task<ClientReadDto?> GetByIdAsync(int id);
        Task<ClientReadDto> CreateAsync(ClientCreateDto dto);
        Task<bool> UpdateAsync(int id, ClientUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
