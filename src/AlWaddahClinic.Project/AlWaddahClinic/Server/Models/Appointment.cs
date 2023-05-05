using System;
namespace AlWaddahClinic.Server.Models
{
	public class Appointment
	{
		public int Id { get; set; }
		public int PatientId { get; set; }
		public Patient Patient { get; set; }
		public DateTime Date { get; set; }
	}
}

		