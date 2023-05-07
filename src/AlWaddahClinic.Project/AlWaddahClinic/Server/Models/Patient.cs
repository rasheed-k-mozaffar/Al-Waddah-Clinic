using System;
namespace AlWaddahClinic.Server.Models
{
	public class Patient : Base
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PhoneNumber { get; set; }
		public string? EmaillAddress { get; set; }
		public string? Address { get; set; }
		public List<HealthRecord>? HealthRecords { get; set; }
		public List<Appointment>? Appointments { get; set; }
	}
}

			