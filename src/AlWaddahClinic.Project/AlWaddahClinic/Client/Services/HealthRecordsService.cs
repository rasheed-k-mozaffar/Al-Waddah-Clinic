﻿using System;
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

        public async Task<ApiResponse<IEnumerable<HealthRecordDto>>> GetRecordsForPatientAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"/api/healthrecords/{patientId}");

            if(!response.IsSuccessStatusCode)
            {
                throw new DomainException("Something went wrong while attempting to retrieve the records");
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<HealthRecordDto>>>();
        }

        public async Task<ApiResponse<HealthRecordDto>> GetHealthRecordByIdAsync(int patientId, int recordId)
        {
            var response = await _httpClient.GetAsync($"/api/healthrecords/{patientId}/{recordId}");

            if(!response.IsSuccessStatusCode)
            {
                throw new DomainException("Something went wrong while attempting to retrieve the record");
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<HealthRecordDto>>();
        }


        public async Task AddRecordAsync(int patientId, HealthRecordCreateDto model)
        {
			var response = await _httpClient.PostAsJsonAsync($"/api/healthrecords/{patientId}", model);

			if (!response.IsSuccessStatusCode)
			{
				throw new DomainException("Something went wrong while creating the new health record");
			}
		}

        public Task<ApiResponse> RemoveRecordAsync(int patientId, int recordId)
        {
            throw new NotImplementedException();
        }
    }
}

