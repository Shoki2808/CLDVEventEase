namespace EventEaseAPI.Services
{
    public class AdminService : GenericService<Admin>
    {
        public AdminService(ApplicationDbContext context) : base(context) { }
    }
}
