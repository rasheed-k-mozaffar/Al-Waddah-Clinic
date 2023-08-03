using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class PaymentCreateDto
	{
		public decimal? Amount { get; set; }
		public string? Description { get; set; }
		public string? Currency { get; set; }
	}
}

