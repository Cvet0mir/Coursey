
using Coursey.ViewModels.Teacher;

namespace Coursey.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherIndexViewModel>> GetAllAsync(
            string? search = null,
            int? age =null,
            string? sortOrder = null);

        Task<TeacherDetailsViewModel?> GetDetailsAsync(int id);

        Task CreateAsync(TeacherCreateViewModel model);

        Task<TeacherEditViewModel?> GetForEditAsync(int id);

        Task EditAsync(TeacherEditViewModel model);

        Task<TeacherDeleteViewModel?> GetForDeleteAsync(int id);

        Task DeleteAsync(int id);
    }
}