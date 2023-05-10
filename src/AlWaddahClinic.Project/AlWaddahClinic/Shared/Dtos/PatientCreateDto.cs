using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class PatientCreateDto
	{
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
    }
}

