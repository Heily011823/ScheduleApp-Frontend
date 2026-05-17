using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ScheduleApp.UI.Models;

namespace ScheduleApp.UI.Services
{
    public class SubjectApiService
    {
        private readonly HttpClient _httpClient;

        public SubjectApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7216/api/") // Mantenemos la misma BaseAddress que User
            };
        }

        public async Task<List<SubjectModel>> GetSubjectsAsync()
        {
            try
            {
                // Consumimos el objeto paginado igual que en los usuarios
                var result = await _httpClient.GetFromJsonAsync<PagedResultModel<SubjectModel>>("subjects")
                             ?? new PagedResultModel<SubjectModel>();

                return result.Items; // Retornamos únicamente la lista de ítems desenvuelta
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en SubjectApiService: {ex.Message}");
                return new List<SubjectModel>();
            }
        }

        public async Task<bool> CreateSubjectAsync(SubjectModel subject)
        {
            var response = await _httpClient.PostAsJsonAsync("subjects", subject);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSubjectAsync(Guid id, SubjectModel subject)
        {
            var response = await _httpClient.PutAsJsonAsync($"subjects/{id}", subject);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"subjects/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}