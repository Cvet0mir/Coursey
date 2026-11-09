using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursey.Data.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 150 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Subject must be between 2 and 150 characters.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Difficulty must be between 2 and 50 characters.")]
        public string? Difficulty { get; set; }

        [Required(ErrorMessage = "Teacher is required.")]
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    }
}
