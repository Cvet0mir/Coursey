using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursey.Data.Models
{
    public class Teacher
    {
        [Key]
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
        [Column("Years of Experience")]
        public int YearsExperience { get; set; }

        [StringLength(150, MinimumLength = 2, ErrorMessage = "Address must be between 2 and 150 characters.")]
        public string Address { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
