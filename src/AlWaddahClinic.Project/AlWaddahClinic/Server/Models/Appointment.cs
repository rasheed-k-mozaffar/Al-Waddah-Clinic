using System;
namespace AlWaddahClinic.Server.Models
{
	public class Appointment : Base
	{
		public int Id { get; set; }
		public int PatientId { get; set; }
		public string? Description { get; set; }
		public virtual Patient Patient { get; set; }
		public DateTime? StartAt { get; set; }
		public DateTime? FinishAt { get; set; }
		public virtual Prescription? Prescription { get; set; }
	}
}

					