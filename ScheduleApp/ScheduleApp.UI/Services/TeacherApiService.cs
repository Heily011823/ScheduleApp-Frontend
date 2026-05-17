using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ScheduleApp.UI.Models;

namespace ScheduleApp.UI.Services;

public class TeacherApiService
{
    private readonly HttpClient _httpClient;

    public TeacherApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7216/api/")
        };
    }

    public async Task<List<TeacherModel>> GetTeachersAsync(
        string? name = null,
        string? status = null)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", SessionService.Token);

        var url = "teachers";
        var query = new List<string>();

        if (!string.IsNullOrWhiteSpace(name))
            query.Add($"name={Uri.EscapeDataString(name)}");
        if (!string.IsNullOrWhiteSpace(status) && status != "Estado")
            query.Add($"status={Uri.EscapeDataString(status)}");

        if (query.Count > 0)
            url += "?" + string.Join("&", query);

        return await _httpClient.GetFromJsonAsync<List<TeacherModel>>(url)
               ?? new List<TeacherModel>();
    }
}