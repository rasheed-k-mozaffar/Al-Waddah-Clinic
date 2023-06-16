using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto mode, Guid clinicId);
        Task<UserManagerResponse> LoginUserAsync(LoginUserDto model);
        Task<UserManagerResponse> AuthorizeUserAsync(string userId);
    }
}

