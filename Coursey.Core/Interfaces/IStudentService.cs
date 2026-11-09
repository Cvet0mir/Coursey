using Coursey.ViewModels.Student;

namespace Coursey.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentIndexViewModel>> GetAllAsync();

        Task<StudentDetailsViewModel?> GetDetailsAsync(int id);

        Task<StudentCreateViewModel> GetCreateModelAsync();

        Task<List<CourseOptionViewModel>> GetCoursesAsync();

        Task CreateAsync(StudentCreateViewModel model);

        Task<StudentEditViewModel?> GetForEditAsync(int id);

        Task EditAsync(StudentEditViewModel model);

        Task<StudentDeleteViewModel?> GetForDeleteAsync(int id);

        Task DeleteAsync(int id);
    }
}