namespace AlWaddahClinic.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> LoginUserAsync(LoginUserDto model);
        Task RegisterClinicAsync(RegisterClinicDto model);
        Task<LoginResult> VerifyUserAsync(string userId);
    }
}