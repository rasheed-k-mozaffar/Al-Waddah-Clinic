using System;
using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly HttpClient _httpClient;
        public PaymentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<PaymentDto>> GetPaymentByIdAsync(Guid paymentId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/payments/payment-details/{paymentId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message!);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<PaymentDto>>();
        }

        public async Task<ApiResponse<IEnumerable<PaymentDto>>> GetPaymentsForRecordAsync(Guid recordId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/payments/{recordId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message!);
            }

            return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<PaymentDto>>>();
        }

        public async Task CreatePaymentAsync(Guid healthRecordId, PaymentCreateDto model)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"/api/payments/{healthRecordId}", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message!);
            }
        }

        public async Task RemovePaymentAsync(Guid paymentId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/payments/{paymentId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message!);
            }
        }
    }
}

