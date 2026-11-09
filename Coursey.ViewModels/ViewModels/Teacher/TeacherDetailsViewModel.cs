namespace Coursey.ViewModels.Teacher
{
    public class TeacherDetailsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MainSubject { get; set; }

        public int YearsExperience { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public List<string> Courses { get; set; } = new();
    }
}