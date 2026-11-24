
using Coursey.ViewModels.Course;
using Coursey.ViewModels.Teacher;

namespace Coursey.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseIndexViewModel>> GetAllAsync();

        Task<List<CourseIndexViewModel>> GetFilteredAsync(
            string? search = null,
            string? subject = null,
            string? difficulty = null,
            string? sortOrder = null);

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
