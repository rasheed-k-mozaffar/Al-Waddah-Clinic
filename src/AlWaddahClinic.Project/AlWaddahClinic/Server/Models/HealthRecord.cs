using System;
namespace AlWaddahClinic.Server.Models
{
	public class HealthRecord
	{	
		public int Id { get; set; }
		public int PatientId { get; set; }
		public Patient Patient { get; set; }

		public string? BloodType { get; set; }
		public List<Medication>? Medications { get; set; }
		public List<string>? Allergies { get; set; }
		public List<string>? SurgicalHistory { get; set; }
	}
}

	