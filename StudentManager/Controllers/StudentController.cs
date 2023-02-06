using Microsoft.AspNetCore.Mvc;
using StudentManager.Core.Data;
using StudentManager.Core.Repository;
using StudentManager.Models;

namespace StudentManager.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepositories _repositories;

        public StudentController(IRepositories repositories)
        {
            _repositories = repositories;
        }

        public IActionResult Index()
        {
            var students = _repositories.StudentRepository.GetStudents();

            return View(students);
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Create()
        {
            var studentViewModel = new StudentViewModel();
            studentViewModel.Student = new Student();
            studentViewModel.Student.Id = Guid.NewGuid().ToString();
            studentViewModel.Student.Grades = new List<Grade>();
            return View(studentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
			var student = await _repositories.StudentRepository.AddNewStudentAsync(studentViewModel.Student);

			return RedirectToAction(nameof(Index));
		}
    }
}
