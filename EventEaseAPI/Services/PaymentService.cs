

namespace EventEaseAPI.Services
{
    public class PaymentService : GenericService<Payment>
    {
        public PaymentService(ApplicationDbContext context) : base(context) { }
    }
}

