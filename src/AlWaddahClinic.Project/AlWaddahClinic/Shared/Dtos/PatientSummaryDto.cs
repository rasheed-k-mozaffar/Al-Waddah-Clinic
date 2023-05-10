using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class PatientSummaryDto
	{
		public int PatientId { get; set; }
		public string FullName { get; set; }
		public string? EmailAddress { get; set; }
	}
}


