using ScheduleApp.UI.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScheduleApp.UI.ViewModels
{
    public class MateriasViewModel
    {
        public ObservableCollection<SubjectModel> Subjects { get; set; }

        private readonly HttpClient _httpClient;

        public MateriasViewModel()
        {
            Subjects = new ObservableCollection<SubjectModel>();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7216/")
            };

            _ = LoadSubjects();
        }

        private async Task LoadSubjects()
        {
            try
            {
                var subjects = await _httpClient
                    .GetFromJsonAsync<ObservableCollection<SubjectModel>>("api/subjects");

                if (subjects != null)
                {
                    Subjects.Clear();

                    foreach (var subject in subjects)
                    {
                        Subjects.Add(subject);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}