using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ScheduleApp.UI.Models;

namespace ScheduleApp.UI.Services;

public class UserApiService
{
    private readonly HttpClient _httpClient;

    public UserApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7216/api/")
        };
    }

    public async Task<List<UserModel>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UserModel>>("users")
               ?? new List<UserModel>();
    }

    public async Task<bool> CreateUserAsync(UserModel user)
    {
        var response = await _httpClient.PostAsJsonAsync("users", user);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateUserAsync(Guid id, UserModel user)
    {
        var response = await _httpClient.PutAsJsonAsync($"users/{id}", user);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"users/{id}");
        return response.IsSuccessStatusCode;
    }
}