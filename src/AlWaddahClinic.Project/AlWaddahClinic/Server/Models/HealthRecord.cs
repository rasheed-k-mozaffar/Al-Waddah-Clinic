using System;
namespace AlWaddahClinic.Server.Models
{
	public class HealthRecord : Base
	{	
		public int Id { get; set; }
		public int PatientId { get; set; }
		public int? AppointmentId { get; set; }
		public string Description { get; set; }
		public virtual Patient Patient { get; set; }
		public virtual Appointment? Appointment { get; set; }
		public virtual List<Note>? Notes { get; set; }
	}
}

		