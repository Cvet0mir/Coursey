using System.ComponentModel.DataAnnotations;

namespace Coursey.ViewModels.Student
{
    public class StudentEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateOnly BirthDate { get; set; }

        [StringLength(100, ErrorMessage = "Favourite subject cannot exceed 100 characters.")]
        [Display(Name = "Favourite Subject")]
        public string? FavouriteSubject { get; set; }

        [Display(Name = "Courses")]
        public List<int> SelectedCourseIds { get; set; } = new();

        public List<CourseOptionViewModel> AvailableCourses { get; set; } = new();
    }
}