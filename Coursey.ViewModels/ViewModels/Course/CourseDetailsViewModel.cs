namespace Coursey.ViewModels.Course
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Difficulty { get; set; }

        public string TeacherName { get; set; }

        public List<string> Students { get; set; } = new List<string>();
    }
}