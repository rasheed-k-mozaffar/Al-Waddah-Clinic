using System;
namespace AlWaddahClinic.Server.Models
{
	public class Prescription : Base
	{
		public Guid Id { get; set; }
		public Guid AppointmentId { get; set; }
		public Guid ClinicId { get; set; }
		public string? Description { get; set; }
		public virtual Appointment Appointment { get; set; }
		public virtual List<Medication> Medications { get; set; }
    }
}	

			