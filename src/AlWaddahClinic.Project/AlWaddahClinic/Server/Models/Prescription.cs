using System;
namespace AlWaddahClinic.Server.Models
{
	public class Prescription : Base
	{
		public int Id { get; set; }
		public int AppointmentId { get; set; }
		public string? Description { get; set; }
		public virtual Appointment Appointment { get; set; }
		public virtual List<Medication> Medications { get; set; }
    }
}	

		