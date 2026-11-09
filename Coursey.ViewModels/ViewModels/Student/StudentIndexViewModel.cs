namespace Coursey.ViewModels.Student
{
    public class StudentIndexViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateOnly BirthDate { get; set; }

        public string? FavouriteSubject { get; set; }

        public List<string> Courses { get; set; } = new();
    }
}