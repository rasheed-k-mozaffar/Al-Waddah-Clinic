using System;
namespace AlWaddahClinic.Server.Models
{
	public class Patient : Base
	{
		public Guid Id { get; set; }
		public Guid ClinicId { get; set; }
		public string FullName { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? PhoneNumber { get; set; }
		public string? EmailAddress { get; set; }
		public string? Address { get; set; }
		public GenderEnum Gender { get; set; }
		public string? MedicalHistory { get; set; }
		public virtual List<HealthRecord>? HealthRecords { get; set; }
		public virtual List<Appointment>? Appointments { get; set; }
	}
}

				