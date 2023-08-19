using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class HealthRecordCreateDto
	{
		public string Description { get; set; }
		public string[]? TeethIds { get; set; }
		public List<NoteCreateDto>? Notes { get; set; }
		public decimal? TotalPayment { get; set; }
		public string? Currency { get; set; }
		public PaymentCreateDto? PaymentCreate { get; set; }
		public List<string>? PatientSuggestion { get; set; }
		public List<string>? SuggestedMedicalTests { get; set; }
		public List<string>? MedicalCaseInsight { get; set; }
		public List<string>? RelatedMedicalCases { get; set; }
	}
}


		