using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class HealthRecordUpdateDto
	{
        public string Description { get; set; }
        public List<NoteUpdateDto>? Notes { get; set; }
        public List<string>? PatientSuggestion { get; set; }
        public List<string>? SuggestedMedicalTests { get; set; }
        public List<string>? MedicalCaseInsight { get; set; }
        public List<string>? RelatedMedicalCases { get; set; }
    }
}

