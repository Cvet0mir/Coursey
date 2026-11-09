
using Coursey.Data;
using Coursey.Data.Models;
using Coursey.Services.Interfaces;
using Coursey.ViewModels.Course;
using Coursey.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace Coursey.Services
{
    public class CourseService : ICourseService
    {
        private readonly CourseyDbContext _context;

        public CourseService(CourseyDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseIndexViewModel>> GetAllAsync()
        {
            var query = _context.Courses
                .Select(c => new CourseIndexViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                    StartDate = DateOnly.FromDateTime(c.StartDate),
                    EndDate = DateOnly.FromDateTime(c.EndDate),
                    Difficulty = c.Difficulty,
                    TeacherName = c.Teacher.FirstName + " " + c.Teacher.LastName
                });

            return await query.ToListAsync();
        }
        public async Task<CourseDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseDetailsViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                    StartDate = DateOnly.FromDateTime(c.StartDate),
                    EndDate = DateOnly.FromDateTime(c.EndDate),
                    Difficulty = c.Difficulty,
                    TeacherName = c.Teacher.FirstName + " " + c.Teacher.LastName,
                    Students = c.CourseStudents
                        .Select(cs => cs.Student.FirstName + " " + cs.Student.LastName)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CourseCreateViewModel model)
        {
            ValidateDates(model.StartDate, model.EndDate);

            var teacherExists = await _context.Teachers.AnyAsync(t => t.Id == model.TeacherId);

            if (!teacherExists)
            {
                throw new InvalidOperationException($"Teacher with ID {model.TeacherId} not found.");
            }

            var course = new Course
            {
                Name = model.Name,
                Subject = model.Subject,
                StartDate = model.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = model.EndDate.ToDateTime(TimeOnly.MinValue),
                Difficulty = model.Difficulty,
                TeacherId = model.TeacherId
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseEditViewModel?> GetForEditAsync(int id)
        {
            var course = await _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseEditViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                    StartDate = DateOnly.FromDateTime(c.StartDate),
                    EndDate = DateOnly.FromDateTime(c.EndDate),
                    Difficulty = c.Difficulty,
                    TeacherId = c.TeacherId
                })
                .FirstOrDefaultAsync();

            if (course == null)
            {
                return null;
            }

            course.Teachers = await GetTeachersAsync();
            return course;
        }

        public async Task EditAsync(CourseEditViewModel model)
        {
            ValidateDates(model.StartDate, model.EndDate);

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (course == null)
            {
                throw new InvalidOperationException($"Course with ID {model.Id} not found.");
            }

            var teacherExists = await _context.Teachers.AnyAsync(t => t.Id == model.TeacherId);
            if (!teacherExists)
            {
                throw new InvalidOperationException($"Teacher with ID {model.TeacherId} not found.");
            }

            course.Name = model.Name;
            course.Subject = model.Subject;
            course.StartDate = model.StartDate.ToDateTime(TimeOnly.MinValue);
            course.EndDate = model.EndDate.ToDateTime(TimeOnly.MinValue);
            course.Difficulty = model.Difficulty;
            course.TeacherId = model.TeacherId;

            await _context.SaveChangesAsync();
        }

        public async Task<CourseDeleteViewModel?> GetForDeleteAsync(int id)
        {
            return await _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseDeleteViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                throw new InvalidOperationException(
                    $"Course with ID {id} not found.");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TeacherDetailsViewModel>> GetTeachersAsync()
        {
            return await _context.Teachers
                .Select(t => new TeacherDetailsViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName
                })
                .ToListAsync();
        }

        private static void ValidateDates(DateOnly startDate, DateOnly endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException(
                    "The course start date must be before the end date.");
            }
        }

        public async Task<CourseCreateViewModel> GetCreateModelAsync()
        {
            return new CourseCreateViewModel
            {
                Teachers = await GetTeachersAsync()
            };
        }


    }
}
