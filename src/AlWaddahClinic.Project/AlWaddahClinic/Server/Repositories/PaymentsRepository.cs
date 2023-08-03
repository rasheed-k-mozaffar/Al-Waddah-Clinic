using System;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Options;

namespace AlWaddahClinic.Server.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
	{
        private readonly ClinicDbContext _context;
        private readonly IdentityOptions _options;

        public PaymentsRepository(ClinicDbContext context, IdentityOptions options)
        {
            _context = context;
            _options = options;
        }

        public Task CreatePaymentAsync(Guid healthRecordId, Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task RemovePaymentAsync(Guid paymentId)
        {
            throw new NotImplementedException();
        }
    }
}

