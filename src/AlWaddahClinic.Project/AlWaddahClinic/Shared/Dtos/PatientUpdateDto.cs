using System;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
	public class PatientUpdateDto
	{
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public GenderEnum Gender { get; set; }
    }
}

