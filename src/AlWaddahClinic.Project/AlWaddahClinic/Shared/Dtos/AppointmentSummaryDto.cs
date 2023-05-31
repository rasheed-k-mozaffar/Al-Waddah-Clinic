using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class AppointmentSummaryDto
	{
		public int Id { get; set; }
		public string PatientName { get; set; }
		public DateTime? StartAt { get; set; }
	}
}	

	