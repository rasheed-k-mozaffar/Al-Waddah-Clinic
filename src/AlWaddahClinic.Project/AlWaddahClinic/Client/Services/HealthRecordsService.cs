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

        public async Task<ApiResponse<IEnumerable<HealthRecordSummaryDto>>> GetRecordsForPatientAsync(Guid patientId)
        {
            var response = await _httpClient.GetAsync($"/api/healthrecords/{patientId}");

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            var records = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<HealthRecordSummaryDto>>>();
            return records;
        }

        public async Task<ApiResponse<HealthRecordDto>> GetHealthRecordByIdAsync(Guid recordId)
        {
            var response = await _httpClient.GetAsync($"/api/healthrecords/record/{recordId}");

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            var record = await response.Content.ReadFromJsonAsync<ApiResponse<HealthRecordDto>>();
            return record;
        }


        public async Task AddRecordAsync(Guid patientId, HealthRecordCreateDto model)
        {
			var response = await _httpClient.PostAsJsonAsync($"/api/healthrecords/{patientId}", model);

			if (!response.IsSuccessStatusCode)
			{
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }
		}

        public async Task RemoveRecordAsync(Guid recordId)
        {
            var response = await _httpClient.DeleteAsync($"/api/healthrecords/{recordId}");

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }
        }

        public async Task UpdateRecordAsync(Guid recordId, HealthRecordUpdateDto model)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/healthrecords/{recordId}", model);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }
        }

        public async Task MarkPaymentsCompletedAsync(Guid recordId)
        {
            var response = await _httpClient.PutAsync($"/api/healthrecords/payment-completed/{recordId}", null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }
        }
    }
}

