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
                throw new DomainException("Something has gone wrong while adding the patient");
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse>();
        }

        public async Task<ApiResponse<IEnumerable<PatientSummaryDto>>> GetAllPatients()
        {
            var response = await _httpClient.GetAsync("/api/patients");

            if (!response.IsSuccessStatusCode)
            {
                throw new DataRetrievalException("Something has gone wrong while accessing the data");
            }

            var patients = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<PatientSummaryDto>>>();
            return patients;
        }

        public async Task<ApiResponse<PatientDto>> GetPatientById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/patients/{id}");

            if(!response.IsSuccessStatusCode)
            {
                throw new NotFoundException($"No patient was found with the ID: {id}");
            }

            var patient = await response.Content.ReadFromJsonAsync<ApiResponse<PatientDto>>();
            return patient;
        }

        public async Task RemovePatient(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/patients/{id}");
        }

        public Task UpdatePatient(int id)
        {
            throw new NotImplementedException();
        }
    }
}

