using System;
namespace AlWaddahClinic.Server.Models
{
	public class HealthRecord
	{	
		public int Id { get; set; }
		public int PatientId { get; set; }
		public Patient Patient { get; set; }
		public string Description { get; set; }
		public List<Medication>? Medications { get; set; }	
	}
}

		