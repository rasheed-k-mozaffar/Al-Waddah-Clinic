using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IAiRepository
	{
		Task<string[]> GetRelatedMedicalCasesAsync(string caseDescription);
		Task<string[]> GetSuggestedMedicalTestsAsync(string caseDescription);
		Task<string[]> GetSuggestionsForPatientAsync(string caseDescription);
		Task<string[]> GetCaseInsightsAsync(string caseDescription);
	}
}

