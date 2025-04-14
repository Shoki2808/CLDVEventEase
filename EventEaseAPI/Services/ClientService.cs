

namespace EventEaseAPI.Services
{
    public class ClientService : GenericService<Client>
    {
        public ClientService(ApplicationDbContext context) : base(context) { }
    }
}
