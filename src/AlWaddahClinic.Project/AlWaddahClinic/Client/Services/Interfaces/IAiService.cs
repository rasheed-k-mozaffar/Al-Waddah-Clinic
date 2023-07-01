using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IAiService
	{
        //TODO : Review the contract
        Task<ApiResponse<string[]>> GetRelatedMedicalCasesAsync(CaseDto model);
		Task<ApiResponse<string[]>> GetSuggestedMedicalTestsAsync(CaseDto model);
		Task<ApiResponse<string[]>> GetSuggestionsForPatientAsync(CaseDto model);
		Task<ApiResponse<string[]>> GetCaseInsightsAsync(CaseDto model);
    }
}

