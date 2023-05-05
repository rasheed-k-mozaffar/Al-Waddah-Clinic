using System;
namespace AlWaddahClinic.Server.Models
{
	public class Prescription
	{
		public int Id { get; set; }
		public int AppointmentId { get; set; }
		public string? Description { get; set; }
		public List<Medication> Medications { get; set; }
	}
}

		