using System;
using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
	public class AnalysisService : IAnalysisService
	{
        private readonly HttpClient _httpClient;

        public AnalysisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<double>>> GetPatientCountByGenderAsync()
        {
            var response = await _httpClient.GetAsync("api/analysis/count-patients");

            if(!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DataRetrievalException(error.Message!);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<List<double>>>();
        }

        public async Task<ApiResponse<List<PatientSummaryDto>>> GetRecentlyAddedPatientsAsync(int numToGet)
        {
            var response = await _httpClient.GetAsync($"api/analysis/recently-added-patients/{numToGet}");

            if (!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DataRetrievalException(error.Message!);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<List<PatientSummaryDto>>>();
        }

        public async Task<ApiResponse<List<AppointmentSummaryDto>>> GetUpcomingAppointmentsAsync(int limit)
        {
            var response = await _httpClient.GetAsync($"api/analysis/get-upcoming-appointments/{limit}");

            if (!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DataRetrievalException(error.Message!);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<List<AppointmentSummaryDto>>>();
        }
    }
}

