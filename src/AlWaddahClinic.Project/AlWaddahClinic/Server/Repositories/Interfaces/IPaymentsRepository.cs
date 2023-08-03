using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IPaymentsRepository
	{
		Task CreatePaymentAsync(Guid healthRecordId, Payment payment);
		Task RemovePaymentAsync(Guid paymentId);
	}
}

