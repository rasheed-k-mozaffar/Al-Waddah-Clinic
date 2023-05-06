using System;
namespace AlWaddahClinic.Server.Models
{
	public class Appointment : Base
	{
		public int Id { get; set; }
		public int PatientId { get; set; }
		public string? Description { get; set; }
		public Patient Patient { get; set; }
		public DateTime StartAt { get; set; }
		public DateTime? FinishAt { get; set; }
		public Prescription? Prescription { get; set; }
	}
}

					