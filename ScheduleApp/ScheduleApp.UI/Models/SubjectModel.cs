namespace ScheduleApp.UI.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Semester { get; set; }

        public int Credits { get; set; }

        public string Status { get; set; } = string.Empty;

        public int WeeklyHours { get; set; }
    }
}