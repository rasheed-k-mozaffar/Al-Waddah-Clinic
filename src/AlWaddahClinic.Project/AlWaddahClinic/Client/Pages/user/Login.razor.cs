using System;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AlWaddahClinic.Client.Pages.User
{
	public partial class Login : ComponentBase
	{
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public IAuthService AuthService { get; set; } = null!;
        [Inject]
        public ILocalStorageService LocalStorage { get; set; } = null!;
        [Inject]
        public AuthenticationStateProvider AuthState { get; set; } = null!;

        private LoginUserDto model = new();

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task LoginUser()
        {
            _isBusy = true;

            //Add the login logic
            try
            {
                var result = await AuthService.LoginUserAsync(model);
                Console.WriteLine("Hello There!");
                Console.WriteLine("--------------");

                Console.WriteLine(result.Token);
                Console.WriteLine(result.HasSucceeded);
                var token = result.Token;

                //Set the JWT value to the access_token key in the local storage.
                await LocalStorage.SetItemAsStringAsync("access_token", token);

                await AuthState.GetAuthenticationStateAsync();

                NavigationManager.NavigateTo("/");
            }
            catch (AuthenticationFailedException ex)
            {
                _errorMessage = ex.Message;
            }

            //Reset the private fields
            _isBusy = false;
            _errorMessage = string.Empty;
        }
    }
}

