namespace AlWaddahClinic.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> LoginUserAsync(LoginUserDto model);

        //TODO Add a register user signature
    }
}