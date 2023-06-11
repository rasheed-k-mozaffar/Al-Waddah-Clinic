using System;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
	public class PatientSummaryDto
	{
		public Guid PatientId { get; set; }
		public string FullName { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhoneNumber { get; set; }
		public GenderEnum Gender { get; set; }
	}
}


