using System.Net.Http.Json;

namespace AlWaddahClinic.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResult> LoginUserAsync(LoginUserDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new AuthenticationFailedException(error.Message);
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>();
            return result.Value;
        }

        public async Task RegisterClinicAsync(RegisterClinicDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", model);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ClinicRegisterationFailedException(error.Message);
            }
        }

        public async Task<LoginResult> VerifyUserAsync(string userId)
        {
            var response = await _httpClient.PutAsync($"/api/auth/{userId}", null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new DomainException(error.Message);
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>();
            return result.Value;
        }
    }
}