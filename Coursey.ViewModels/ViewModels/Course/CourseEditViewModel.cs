using System.ComponentModel.DataAnnotations;
using Coursey.ViewModels.Teacher;

namespace Coursey.ViewModels.Course
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The course name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The course name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The course subject is required.")]
        [StringLength(100, ErrorMessage = "The course subject must be no more than 100 characters.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "The course start date is required.")]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "The course end date is required.")]
        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

        [StringLength(50)]
        public string? Difficulty { get; set; }

        [Required(ErrorMessage = "The teacher is required.")]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }

        public List<TeacherDetailsViewModel> Teachers { get; set; } = new List<TeacherDetailsViewModel>();
    }
}