
using Coursey.ViewModels.Course;
using Coursey.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coursey.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseIndexViewModel>> GetAllAsync();

        Task<CourseDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateAsync(CourseCreateViewModel model);

        Task<CourseEditViewModel?> GetForEditAsync(int id);

        Task EditAsync(CourseEditViewModel model);

        Task<CourseDeleteViewModel?> GetForDeleteAsync(int id);

        Task DeleteAsync(int id);

        Task<List<TeacherDetailsViewModel>> GetTeachersAsync();

        Task<CourseCreateViewModel> GetCreateModelAsync();
    }
}
