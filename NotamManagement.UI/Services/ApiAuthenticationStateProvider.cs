using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using NotamManagement.Core.Models;

namespace NotamManagement.UI.Services

{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider

    {
        private readonly HttpClient _httpClient;

        public ApiAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginAsync(string email, string password)
        {
            var loginData = new
            {
                email = email,
                password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/login?useCookies=true", loginData);

            if (response.IsSuccessStatusCode)
            {
                // Notify Blazor of the authentication state change
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
            else
            {
                // Handle login failure (you can add error handling/logging as needed)
                throw new Exception("Login failed");
            }
        }

        public async Task LogoutAsync()
        {
            await _httpClient.PostAsync("/api/auth/logout", null);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }



        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Check if user is authenticated by calling an API endpoint.
                var isAuthenticated = await _httpClient.GetFromJsonAsync<bool>("/api/user/isAuth");

                if (isAuthenticated)
                {
                    
                    // If authenticated, return a principal with an empty identity (no claims).
                    var identity = new System.Security.Claims.ClaimsIdentity("apiAuthType");
                    var user = new System.Security.Claims.ClaimsPrincipal(identity);
                    return new AuthenticationState(user);
                }
            }
            catch
            {
                // If an error occurs or the user is not authenticated, continue to return an unauthenticated state.
            }

            // Return an unauthenticated state (empty principal) if not authenticated.
            return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity()));
        }
    }

}
