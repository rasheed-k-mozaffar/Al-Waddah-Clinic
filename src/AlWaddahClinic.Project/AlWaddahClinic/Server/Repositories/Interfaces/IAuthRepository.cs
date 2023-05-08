using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model);
        Task<UserManagerResponse> LoginUserAsync(LoginUserDto model);
    }
}

