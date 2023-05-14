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

        public Task<ApiResponse<PatientDto>> GetPatientById(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemovePatient(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePatient(int id)
        {
            throw new NotImplementedException();
        }
    }
}

