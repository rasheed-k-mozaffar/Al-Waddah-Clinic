using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IAuthRepository
	{
		Task<ApiAuthResponse> RegisterUserAsync(RegisterUserDto model);
		Task<ApiAuthResponse> LoginUserAsync(LoginUserDto model);
	}
}

