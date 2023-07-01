using System;
using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;

        public AiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region ContractImplementaion
        public async Task<ApiResponse<string[]>> GetCaseInsightsAsync(CaseDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/insights/IllnessInfo", model);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new InsightsRetrievalException(error.Message);
            }

            var insights = await response.Content.ReadFromJsonAsync<ApiResponse<string[]>>();
            return insights;
        }

        public async Task<ApiResponse<string[]>> GetRelatedMedicalCasesAsync(CaseDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/insights/RelatedMedicalCases", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new InsightsRetrievalException(error.Message);
            }

            var insights = await response.Content.ReadFromJsonAsync<ApiResponse<string[]>>();
            return insights;
        }

        public async Task<ApiResponse<string[]>> GetSuggestedMedicalTestsAsync(CaseDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/insights/SuggestedTests", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new InsightsRetrievalException(error.Message);
            }

            var insights = await response.Content.ReadFromJsonAsync<ApiResponse<string[]>>();
            return insights;
        }

        public async Task<ApiResponse<string[]>> GetSuggestionsForPatientAsync(CaseDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/insights/PatientSuggestions", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new InsightsRetrievalException(error.Message);
            }

            var insights = await response.Content.ReadFromJsonAsync<ApiResponse<string[]>>();
            return insights; ;
        }
        #endregion
    }
}

