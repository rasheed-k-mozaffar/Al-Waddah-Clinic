using System;
using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly HttpClient _httpClient;

        public PatientsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> AddPatient(PatientCreateDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/patients", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }

        public async Task<ApiResponse<IEnumerable<PatientSummaryDto>>> GetAllPatients()
        {
            var response = await _httpClient.GetAsync("/api/patients");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            var patients = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<PatientSummaryDto>>>();
            return patients;
        }

        public async Task<ApiResponse<PatientDto>> GetPatientById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/patients/{id}");

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            var patient = await response.Content.ReadFromJsonAsync<ApiResponse<PatientDto>>();
            return patient;
        }

        public async Task RemovePatient(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/patients/{id}");
        }

        public async Task UpdatePatient(int id, PatientUpdateDto model)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/patients/{id}", model);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }
        }
    }
}

