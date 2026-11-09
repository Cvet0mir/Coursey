using Coursey.Data;
using Coursey.Data.Models;
using Coursey.Services.Interfaces;
using Coursey.ViewModels.Student;
using Microsoft.EntityFrameworkCore;

namespace Coursey.Services
{
    public class StudentService : IStudentService
    {
        private readonly CourseyDbContext _context;

        public StudentService(CourseyDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentIndexViewModel>> GetAllAsync()
        {
            return await _context.Students
                .Select(s => new StudentIndexViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    BirthDate = s.BirthDate,
                    FavouriteSubject = s.FavouriteSubject,
                    Courses = s.CourseStudents
                        .Select(cs => cs.Course.Name)
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<StudentDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await _context.Students
                .Where(s => s.Id == id)
                .Select(s => new StudentDetailsViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    BirthDate = s.BirthDate,
                    FavouriteSubject = s.FavouriteSubject,
                    Courses = s.CourseStudents
                        .Select(cs => cs.Course.Name)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<StudentCreateViewModel> GetCreateModelAsync()
        {
            return new StudentCreateViewModel
            {
                AvailableCourses = await GetCoursesAsync()
            };
        }

        public async Task<List<CourseOptionViewModel>> GetCoursesAsync()
        {
            return await _context.Courses
                .Select(c => new CourseOptionViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject
                })
                .ToListAsync();
        }

        public async Task CreateAsync(StudentCreateViewModel model)
        {
            ValidateBirthDate(model.BirthDate);

            var selectedCourses = await GetSelectedCoursesAsync(model.SelectedCourseIds);

            var student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                BirthDate = model.BirthDate,
                FavouriteSubject = model.FavouriteSubject
            };

            foreach (var course in selectedCourses)
            {
                student.CourseStudents.Add(new CourseStudent
                {
                    Course = course
                });
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentEditViewModel?> GetForEditAsync(int id)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Select(s => new StudentEditViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    BirthDate = s.BirthDate,
                    FavouriteSubject = s.FavouriteSubject,
                    SelectedCourseIds = s.CourseStudents
                        .Select(cs => cs.CourseId)
                        .ToList()
                })
                .FirstOrDefaultAsync();
            if (student == null)
            {
                return null;
            }

            student.AvailableCourses = await GetCoursesAsync();
            return student;
        }

        public async Task EditAsync(StudentEditViewModel model)
        {
            ValidateBirthDate(model.BirthDate);

            var student = await _context.Students
                .Include(s => s.CourseStudents)
                .FirstOrDefaultAsync(s => s.Id == model.Id);

            if (student == null)
            {
                throw new InvalidOperationException("Student was not found.");
            }

            var selectedCourses = await GetSelectedCoursesAsync(model.SelectedCourseIds);
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.BirthDate = model.BirthDate;
            student.FavouriteSubject = model.FavouriteSubject;

            _context.CourseStudents.RemoveRange(student.CourseStudents);
            student.CourseStudents = selectedCourses
                .Select(course => new CourseStudent
                {
                    StudentId = student.Id,
                    CourseId = course.Id
                })
                .ToList();
            await _context.SaveChangesAsync();
        }

        public async Task<StudentDeleteViewModel?> GetForDeleteAsync(int id)
        {
            return await _context.Students
                .Where(s => s.Id == id)
                .Select(s => new StudentDeleteViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName
                })
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new InvalidOperationException("Student was not found.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        private async Task<List<Course>> GetSelectedCoursesAsync(List<int> selectedCourseIds)
        {
            var courseIds = selectedCourseIds.Distinct().ToList();
            var courses = await _context.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToListAsync();

            if (courses.Count != courseIds.Count)
            {
                throw new InvalidOperationException("One or more selected courses do not exist.");
            }
            return courses;
        }

        private void ValidateBirthDate(DateOnly birthDate)
        {
            if (birthDate == default)
            {
                throw new ArgumentException("Birth date is required.");
            }

            if (birthDate >= DateOnly.FromDateTime(DateTime.Today))
            {
                throw new ArgumentException("Birth date must be earlier than today.");
            }
        }
    }
}