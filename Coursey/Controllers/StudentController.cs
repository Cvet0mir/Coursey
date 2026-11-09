using Coursey.Services.Interfaces;
using Coursey.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Coursey.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();
            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetDetailsAsync(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _studentService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCourses = await _studentService.GetCoursesAsync();
                return View(model);
            }

            try
            {
                await _studentService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(model.BirthDate), ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.SelectedCourseIds), ex.Message);
            }

            model.AvailableCourses = await _studentService.GetCoursesAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetForEditAsync(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentEditViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                model.AvailableCourses = await _studentService.GetCoursesAsync();
                return View(model);
            }

            try
            {
                await _studentService.EditAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(model.BirthDate), ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.SelectedCourseIds), ex.Message);
            }

            model.AvailableCourses = await _studentService.GetCoursesAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetForDeleteAsync(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _studentService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}