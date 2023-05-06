using System;
namespace AlWaddahClinic.Server.Models
{
	public class Prescription : Base
	{
		public int Id { get; set; }
		public int AppointmentId { get; set; }
		public string? Description { get; set; }
		public Appointment Appointment { get; set; }
		public List<Medication> Medications { get; set; }
    }
}	

		