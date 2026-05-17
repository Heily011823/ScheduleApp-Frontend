using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ScheduleApp.UI.Services;

public class ProgramApiService
{
    private readonly HttpClient _httpClient;

    public ProgramApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7216/api/")
        };
    }

    public async Task<bool> ExportPdfAsync(string savePath)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", SessionService.Token);

        var response = await _httpClient.GetAsync("academicprograms/export/pdf");

        if (!response.IsSuccessStatusCode)
            return false;

        var bytes = await response.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(savePath, bytes);
        return true;
    }
}