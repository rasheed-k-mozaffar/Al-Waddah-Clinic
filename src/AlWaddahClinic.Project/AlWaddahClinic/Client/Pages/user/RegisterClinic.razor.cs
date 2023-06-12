using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.User
{
	public partial class RegisterClinic : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		private RegisterClinicDto model = new();

		private string _country = "🇸🇾 Syria";
		private string _errorMessage = string.Empty;
		private bool _isMakingRequest = false;

		private DateTime? graduationDate;
		private RegisterUserDto userModel = new();

		private async Task RegisterAsync()
		{

		}

	}
}

