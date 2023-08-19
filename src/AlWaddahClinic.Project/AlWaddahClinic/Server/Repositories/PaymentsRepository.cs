using System;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Models;
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

        public async Task<IEnumerable<Payment>> GetPaymentsForHealthRecordAsync(Guid healthRecordId)
        {
            var record = await _context.HealthRecords.FindAsync(healthRecordId);

            if(record == null)
            {
                throw new NotFoundException("The health record was not found");
            }

            var payments = await _context.Payments
                .Where(p => p.HealthRecordId == healthRecordId)
                .ToListAsync();

            return payments;
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == paymentId && p.ClinicId.ToString() == _options.ClinicId);

            if (payment == null)
            {
                throw new NotFoundException("Payment was not found");
            }

            return payment;
        }

        public async Task CreatePaymentAsync(Guid healthRecordId, Payment payment)
        {
            var healthRecord = await _context.HealthRecords.FindAsync(healthRecordId);

            if(healthRecord != null)
            {
                payment.HealthRecord = healthRecord;
                payment.HealthRecordId = healthRecordId;
                payment.ClinicId = Guid.Parse(_options.ClinicId);
                payment.Currency = healthRecord.Currency;

                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("The health record was not found");
            }
        }

        public async Task RemovePaymentAsync(Guid paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);

            if (payment == null)
            {
                throw new NotFoundException("Payment was not found");
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}

