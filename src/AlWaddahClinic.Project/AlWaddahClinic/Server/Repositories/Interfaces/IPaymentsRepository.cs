using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IPaymentsRepository
	{
		Task<IEnumerable<Payment>> GetPaymentsForHealthRecordAsync(Guid healthRecordId);
		Task<Payment> GetPaymentByIdAsync(Guid paymentId);
 		Task CreatePaymentAsync(Guid healthRecordId, Payment payment);
		Task RemovePaymentAsync(Guid paymentId);
	}
}

