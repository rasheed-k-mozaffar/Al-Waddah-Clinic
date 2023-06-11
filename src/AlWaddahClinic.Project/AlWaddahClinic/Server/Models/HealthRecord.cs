using System;
namespace AlWaddahClinic.Server.Models
{
	public class HealthRecord : Base
	{	
		public Guid Id { get; set; }
		public Guid PatientId { get; set; }
		public Guid? AppointmentId { get; set; }
		public Guid ClinicId { get; set; }
		public string Description { get; set; }
		public virtual Patient Patient { get; set; }
		public virtual Appointment? Appointment { get; set; }
		public virtual List<Note>? Notes { get; set; }
	}
}


