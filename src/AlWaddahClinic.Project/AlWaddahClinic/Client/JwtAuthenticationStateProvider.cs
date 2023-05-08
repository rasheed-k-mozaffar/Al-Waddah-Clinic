using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AlWaddahClinic.Client
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public JwtAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _localStorage.ContainKeyAsync("access_token"))
            {
                //The user is logged in and there's an access token in the local storage
                var jwt = await _localStorage.GetItemAsStringAsync("access_token");

                //Read the token to fetch the claims from it
                var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

                var identity = new ClaimsIdentity(token.Claims, "Bearer");

                var user = new ClaimsPrincipal(identity);

                var authState = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(authState));

                return authState;
            }

            //The user is not logged in and there's no access token
            var anonymousUser = new ClaimsPrincipal();
            var anonymousAuthState = new AuthenticationState(anonymousUser);

            return anonymousAuthState;
        }
    }
}