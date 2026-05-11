using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace ScheduleApp.UI.Services
{
    public class FakeAuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public string Role { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;

        public FakeAuthenticationService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7216/api/")
            };
        }

        public bool Login(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password))
                {
                    return false;
                }

                var request = new
                {
                    login = username,
                    password = password
                };

                var response = _httpClient
                    .PostAsJsonAsync("Auth/login", request)
                    .Result;

                if (!response.IsSuccessStatusCode)
                    return false;

                var result = response.Content
                    .ReadFromJsonAsync<LoginResponseModel>()
                    .Result;

                if (result == null)
                    return false;

                Role = result.Role;
                UserName = result.UserName;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private class LoginResponseModel
        {
            public string AccessToken { get; set; } = string.Empty;
            public string TokenType { get; set; } = string.Empty;
            public DateTime ExpiresAt { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }
    }
}