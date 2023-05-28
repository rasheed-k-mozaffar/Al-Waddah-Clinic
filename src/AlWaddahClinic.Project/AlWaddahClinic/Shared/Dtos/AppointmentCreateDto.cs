using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class AppointmentCreateDto
	{
		public DateTime StartAt { get; set; }
		public DateTime? FinishAt { get; set; }
	}
}
	
