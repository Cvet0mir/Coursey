using Coursey.Data;
using Coursey.Data.Models;
using Coursey.Services.Interfaces;
using Coursey.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace Coursey.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly CourseyDbContext _context;

        public TeacherService(CourseyDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherIndexViewModel>> GetAllAsync(string? search = null, int? age = null, string? sortOrder = null)
        {
            var query = _context.Teachers.Select(t => new TeacherIndexViewModel
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                MainSubject = t.MainSubject,
                YearsExperience = t.YearsExperience,
                Age = t.Age
            });

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.FirstName.Contains(search) || t.LastName.Contains(search));
            }

            if (age.HasValue)
            {
                query = query.Where(t => t.Age >= age.Value);
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "firstName":
                        query = query.OrderBy(t => t.FirstName);
                        break;
                    case "lastName":
                        query = query.OrderBy(t => t.LastName);
                        break;
                    case "age asc":
                        query = query.OrderBy(t => t.Age);
                        break;
                    case "age desc":
                        query = query.OrderByDescending(t => t.Age);
                        break;
                    default:
                        query = query.OrderBy(t => t.FirstName);
                        break;
                }
            }

            return await query.ToListAsync();
        }

        public async Task<TeacherDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await _context.Teachers
                .Where(t => t.Id == id)
                .Select(t => new TeacherDetailsViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    MainSubject = t.MainSubject,
                    YearsExperience = t.YearsExperience,
                    Address = t.Address,
                    Age = t.Age,
                    Courses = t.Courses
                        .Select(c => c.Name)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }
        public async Task CreateAsync(TeacherCreateViewModel model)
        {
            var teacher = new Teacher
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MainSubject = model.MainSubject,
                YearsExperience = model.YearsExperience,
                Address = model.Address,
                Age = model.Age
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }
        public async Task<TeacherEditViewModel?> GetForEditAsync(int id)
        {
            return await _context.Teachers
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TeacherEditViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    MainSubject = t.MainSubject,
                    YearsExperience = t.YearsExperience,
                    Address = t.Address,
                    Age = t.Age
                })
                .FirstOrDefaultAsync();
        }
        public async Task EditAsync(TeacherEditViewModel model)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == model.Id);

            if (teacher == null)
            {
                throw new InvalidOperationException($"Teacher with ID {model.Id} not found.");
            }

            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.MainSubject = model.MainSubject;
            teacher.YearsExperience = model.YearsExperience;
            teacher.Address = model.Address;
            teacher.Age = model.Age;

            await _context.SaveChangesAsync();
        }

        public async Task<TeacherDeleteViewModel?> GetForDeleteAsync(int id)
        {
            return await _context.Teachers
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TeacherDeleteViewModel
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName
                })
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                throw new InvalidOperationException($"Teacher with ID {id} not found.");
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }
    }
}