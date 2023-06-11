using System;
using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
	public class AppointmentsService : IAppointmentsService
	{
        private readonly HttpClient _httpClient;

        public AppointmentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ApiResponse<IEnumerable<AppointmentSummaryDto>>> GetAllAppointmentsAsync()
        {
            var response = await _httpClient.GetAsync("/api/appointments");

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponse>();
                throw new DomainException(error.Message);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<AppointmentSummaryDto>>>();
        }

        public async Task<ApiResponse<AppointmentDto>> GetAppointmentByIdAsync(Guid appointmentId)
        {
            var response = await _httpClient.GetAsync($"/api/appointments/{appointmentId}");

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponse>();
                throw new DomainException(error.Message);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<AppointmentDto>>();
        }

        public async Task AddAppointmentAsync(Guid patientId, AppointmentCreateDto model)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/appointments/{patientId}", model);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponse>();
                throw new DomainException(error.Message);
            }
        }
        public async Task RemoveAppointmentAsync(Guid appointmentId)
        {
            var response = await _httpClient.DeleteAsync($"/api/appointments/{appointmentId}");

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponse>();
                throw new DomainException(error.Message);
            }
        }

        public async Task UpdateAppointmentAsync(Guid appointmentId, AppointmentUpdateDto model)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/appointments/{appointmentId}", model);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponse>();
                throw new DomainException(error.Message);
            }
        }

        public async Task CompleteAppointmentAsync(Guid appointmentId, AppointmentStatusCheckDto model)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/appointments/completeappointment/{appointmentId}", model);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }
        }
    }
}

