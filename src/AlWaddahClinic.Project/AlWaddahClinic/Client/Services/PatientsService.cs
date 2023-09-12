using System;
using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly HttpClient _httpClient;
        private readonly IAiService _aiService;

        public PatientsService(HttpClient httpClient, IAiService aiService) {
            _httpClient = httpClient;
            _aiService = aiService;
        }

        public async Task<ApiResponse> AddPatient(PatientCreateDto model) {
            if (model.MedicalHistory is not null && model.MedicalHistory.Any()) {
                CaseDto patientCase = new() {
                    message = string.Join(',', model.MedicalHistory)
                };

                var result = await _aiService.GetSuggestionsForPatientAsync(patientCase);

                if (result.IsSuccess) {
                    model.Suggestions = result.Value.ToList();
                }
            }
            else {
                model.MedicalHistory = null;
                model.Suggestions = null;
            }

            var response = await _httpClient.PostAsJsonAsync("/api/patients", model);

            if (!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }

        public async Task<ApiResponse<IEnumerable<PatientSummaryDto>>> GetAllPatients() {
            var response = await _httpClient.GetAsync("/api/patients");

            if (!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            var patients = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<PatientSummaryDto>>>();
            return patients;
        }

        public async Task<ApiResponse<PatientDto>> GetPatientById(Guid id) {
            var response = await _httpClient.GetAsync($"/api/patients/{id}");

            if (!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            var patient = await response.Content.ReadFromJsonAsync<ApiResponse<PatientDto>>();
            return patient;
        }

        public async Task RemovePatient(Guid id) {
            var response = await _httpClient.DeleteAsync($"/api/patients/{id}");
        }

        public async Task<ApiResponse<IEnumerable<PatientSummaryDto>>> SearchForPatients(string searchText) {
            var response = await _httpClient.GetAsync($"/api/patients/find?searchText={searchText}");

            if (!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new NotFoundException(error.Message);
            }

            var patientsFound = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<PatientSummaryDto>>>();
            return patientsFound;
        }

        public async Task UpdatePatient(Guid id, PatientUpdateDto model) {
            if (model.MedicalHistory is not null && model.MedicalHistory.Any()) {
                CaseDto patientCase = new() {
                    message = string.Join(',', model.MedicalHistory)
                };

                if(!string.IsNullOrEmpty(patientCase.message)) {
                    var result = await _aiService.GetSuggestionsForPatientAsync(patientCase);

                    if (result.IsSuccess) {
                        model.Suggestions = result.Value.ToList();
                    }
                }
            }
            else {
                model.MedicalHistory = null;
                model.Suggestions = null;
            }

            var response = await _httpClient.PutAsJsonAsync($"/api/patients/{id}", model);

            if (!response.IsSuccessStatusCode) {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }
        }
    }
}

