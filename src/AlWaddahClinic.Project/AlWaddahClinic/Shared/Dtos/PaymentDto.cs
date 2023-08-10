using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class PaymentDto
	{
		public Guid Id { get; set; }
		public Guid HealthRecordId { get; set; }
		public string? Description { get; set; }
		public decimal? Amount { get; set; }
		public string? Currency { get; set; }
		public DateTime? PaidOn { get; set; }
	}
}

