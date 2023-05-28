using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class AppointmentUpdateDto
	{
		public DateTime StartAt { get; set; }
		public DateTime? FinishAt { get; set; }
	}
}

	