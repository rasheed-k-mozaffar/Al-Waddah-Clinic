using System;
using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
	public class HealthRecordsService : IHealthRecordsService
	{
        private readonly HttpClient _httpClient;

        public HealthRecordsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IEnumerable<HealthRecordSummaryDto>>> GetRecordsForPatientAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"/api/healthrecords/{patientId}");

            if(!response.IsSuccessStatusCode)
            {
                throw new DomainException("Something went wrong while attempting to retrieve the records");
            }

            var records = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<HealthRecordSummaryDto>>>();
            return records;
        }

        public async Task<ApiResponse<HealthRecordDto>> GetHealthRecordByIdAsync(int recordId)
        {
            var response = await _httpClient.GetAsync($"/api/healthrecords/record/{recordId}");

            if(!response.IsSuccessStatusCode)
            {
                throw new DomainException("Something went wrong while attempting to retrieve the record");
            }

            var record = await response.Content.ReadFromJsonAsync<ApiResponse<HealthRecordDto>>();
            return record;
        }


        public async Task AddRecordAsync(int patientId, HealthRecordCreateDto model)
        {
			var response = await _httpClient.PostAsJsonAsync($"/api/healthrecords/{patientId}", model);

			if (!response.IsSuccessStatusCode)
			{
				throw new DomainException("Something went wrong while creating the new health record");
			}
		}

        public async Task RemoveRecordAsync(int recordId)
        {
            var response = await _httpClient.DeleteAsync($"/api/healthrecords/{recordId}");

            if(!response.IsSuccessStatusCode)
            {
                throw new DomainException("Something went wrong while deleting the health record");
            }
        }
    }
}

