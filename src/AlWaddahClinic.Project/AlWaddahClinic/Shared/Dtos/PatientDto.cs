using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class PatientDto
	{
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmaillAddress { get; set; }
        public string? Address { get; set; }
        public List<HealthRecordDto>? HealthRecords { get; set; }
        public List<AppointmentDto>? Appointments { get; set; }
    }
}

