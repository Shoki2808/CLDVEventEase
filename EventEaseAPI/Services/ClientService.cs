using AutoMapper;
using EventEaseAPI.Data.ApplicationDbContext;
using EventEaseAPI.DTOs.Client;
using EventEaseAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientReadDto>> GetAllAsync()
        {
            var clients = await _context.Clients.ToListAsync();
            return _mapper.Map<IEnumerable<ClientReadDto>>(clients);
        }

        public async Task<ClientReadDto?> GetByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client == null ? null : _mapper.Map<ClientReadDto>(client);
        }

        public async Task<ClientReadDto> CreateAsync(ClientCreateDto dto)
        {
            var client = _mapper.Map<Client>(dto);

            await _context.Clients.AddAsync(client);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to create client due to database error.", ex);
            }

            return _mapper.Map<ClientReadDto>(client);
        }

        public async Task<bool> UpdateAsync(int id, ClientUpdateDto dto)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return false;

            _mapper.Map(dto, client);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Failed to update client due to concurrency issue.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Database error while updating the client.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return false;

            _context.Clients.Remove(client);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Could not delete the client due to a database error.", ex);
            }
        }
    }
}
