
using Coursey.Services.Interfaces;
using Coursey.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace Coursey.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetDetailsAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _courseService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teachers = await _courseService.GetTeachersAsync();
                return View(model);
            }

            try
            {
                await _courseService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(model.EndDate), ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.TeacherId), ex.Message);
            }

            model.Teachers = await _courseService.GetTeachersAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetForEditAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CourseEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                model.Teachers = await _courseService.GetTeachersAsync();
                return View(model);
            }

            try
            {
                await _courseService.EditAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(model.EndDate), ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.TeacherId), ex.Message);
            }

            model.Teachers = await _courseService.GetTeachersAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetForDeleteAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _courseService.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
