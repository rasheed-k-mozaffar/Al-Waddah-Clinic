using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IPaymentsService
	{
		Task<ApiResponse<IEnumerable<PaymentDto>>> GetPaymentsForRecordAsync(Guid recordId);
		Task<ApiResponse<PaymentDto>> GetPaymentByIdAsync(Guid paymentId);
		Task CreatePaymentAsync(Guid healthRecordId, PaymentCreateDto model);
		Task RemovePaymentAsync(Guid paymentId);
	}
}

