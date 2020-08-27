using System;
using CarePortal.API.Entities;

namespace CarePortal.API.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentMethodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IPaymentMethodRepository : IDisposable
    {
    }
}
