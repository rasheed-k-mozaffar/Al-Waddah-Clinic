using System;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
	public class AppointmentSummaryDto
	{
		public Guid Id { get; set; }
		public Guid PatientId { get; set; }
		public string PatientName { get; set; }
		public AppointmentStatusEnum? Status { get; set; }
		public DateTime? StartAt { get; set; }
	}
}	

		