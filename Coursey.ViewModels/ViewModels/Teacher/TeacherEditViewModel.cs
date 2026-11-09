using System.ComponentModel.DataAnnotations;

namespace Coursey.ViewModels.Teacher
{
    public class TeacherEditViewModel
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Main subject is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Main subject must be between 2 and 100 characters.")]
        public string MainSubject { get; set; }

        [Required(ErrorMessage = "Years of experience is required.")]
        [Range(0, 100, ErrorMessage = "Years of experience must be between 0 and 100.")]
        public int YearsExperience { get; set; }

        [StringLength(150, MinimumLength = 2, ErrorMessage = "Address must be between 2 and 150 characters.")]
        public string Address { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }
    }
}