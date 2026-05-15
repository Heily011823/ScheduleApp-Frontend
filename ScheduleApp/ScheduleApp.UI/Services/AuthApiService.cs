using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScheduleApp.UI.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public string Role { get; private set; } = string.Empty;
        public string Token { get; private set; } = string.Empty;

        public AuthApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7216/api/")
            };
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Login = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("auth/login", request);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result == null)
                return false;

            Token = result.Token;
            Role = result.Role;

            return true;
        }

        private class LoginRequest
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        private class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }
    }
}