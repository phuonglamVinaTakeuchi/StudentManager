﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManager.Core.Data;
using StudentManager.Core.Repository;
using StudentManager.Models;

namespace StudentManager.Controllers
{
	public class StudentGradeViewModel
	{
		public string StudentId { get; set; }
		public string GradeId { get; set; }
	}
	public class GradeController : Controller
	{
		private IRepositories _repositories;
		public GradeController(IRepositories repositories)
		{
			_repositories = repositories;
		}

		public Task<IActionResult> Index()
		{
			var grades = _repositories.GradeRepository.GetGrades();
			return Task.FromResult<IActionResult>(View(grades));
		}

		public async Task<IActionResult> Edit(string id)
		{
			var grade = await _repositories.GradeRepository.GetGradeByIdAsync(id);

			var gradeViewModel = new GradeViewModel();
			gradeViewModel.Grade = grade;

			return View(gradeViewModel);
		}

		[HttpPost]
		public IActionResult Edit(GradeViewModel gradeViewModel)
		{
			if (ModelState.IsValid)
			{
				_repositories.GradeRepository.UpdateGradeAsync(gradeViewModel);
				return RedirectToAction("Index");
			}

			return View(gradeViewModel);
		}

		public IActionResult Create()
		{
			var students = _repositories.StudentRepository.GetStudents();

			var selectedStudentIds = new List<string>();

			var selecListStudents = students.Select(st => new SelectListItem(st.Name,st.Id,false));

			var gradeModel = new GradeViewModel()
			{
				Grade = new Grade()
				{
					Id = Guid.NewGuid().ToString(),
					Students = new List<Student>(),
				},
				Students = selecListStudents.ToList(),
				StudentIds = selectedStudentIds
			};

			return View(gradeModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(GradeViewModel gradeViewModel)
		{
			//if (ModelState.IsValid)
			//{
				var grade = await _repositories.GradeRepository.AddNewGradeAsync(gradeViewModel);

				return RedirectToAction(nameof(Index));
			//}

			//return View(gradeViewModel);
		}

	}
}
