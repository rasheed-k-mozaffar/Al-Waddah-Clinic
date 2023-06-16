using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.User
{
	public partial class RegisterClinic : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Inject]
		public IAuthService AuthService { get; set; } = default!;

		private RegisterClinicDto model = new();

		private string _errorMessage = string.Empty;
		private bool _isMakingRequest = false;

		private DateTime? graduationDate;

		private async Task RegisterAsync()
		{
			_isMakingRequest = true;
			_errorMessage = string.Empty;

			try
			{
				await AuthService.RegisterClinicAsync(model);
				NavigationManager.NavigateTo("/user/login");

            }
			catch(ClinicRegisterationFailedException ex)
			{
				_errorMessage = ex.Message;
			}

			_isMakingRequest = false;
		}

	}
}

