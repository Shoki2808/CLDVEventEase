using EventEaseBooking.Models;

namespace EventEaseBooking.Interfaces
{
    public interface IClientService
    {
        public Task<Client> AddClient(Client clientModel);
        public Task<List<Client>> GetClients();
    }
}
