using System;
using System.Text.Json.Serialization;

namespace ScheduleApp.UI.Models
{
    public class SubjectModel
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }

        [JsonPropertyName("Code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("Semester")]
        public int Semester { get; set; }

        [JsonPropertyName("Credits")]
        public int Credits { get; set; }

        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("WeeklyHours")]
        public int WeeklyHours { get; set; }

        public string Status => IsActive ? "Activo" : "Inactivo";
    }
}