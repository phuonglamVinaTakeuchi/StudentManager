using Microsoft.EntityFrameworkCore;
using StudentManager.Areas.Identity.Data;
using StudentManager.Core.Data;
using StudentManager.Core.Repository;
using StudentManager.Models;

namespace StudentManager.Repository
{
	public class GradeRepository : RepositoryBase, IGradeRepository
	{
		private IStudentRepository _studentRepository;
		public GradeRepository(
			StudentManagerContext dataContext,
			IStudentRepository studentRepository)
			: base(dataContext)
		{
			_studentRepository = studentRepository;
		}
		public IEnumerable<Grade> GetGrades()
		{
			var grades = DataContext.Grades
				.Include(g=>g.Students)
				//.ThenInclude(s=>s.Student)
				.ToList();
			return grades;
		}

		public async Task<Grade?> GetGradeByIdAsync(string gradeId)
		{
			return await DataContext.Grades.FirstOrDefaultAsync(x => Equals(x.Id, gradeId));
		}

		public async Task<Grade> UpdateGradeAsync(GradeViewModel editGrade)
		{

			return await UpdateGradeAsync(editGrade.Grade);
		}

		public async Task<Grade> UpdateGradeAsync(Grade editGrade)
		{
			//var dbGrade = await GetGradeByIdAsync(editGrade.Id);

			//if (dbGrade == null)
			//{
			//	return editGrade;
			//}

			//dbGrade.Name = editGrade.Name;
			//dbGrade.Description = editGrade.Description;

			//var addStudents = editGrade
			//	.Students
			//	.Where(x => dbGrade.Students.All(s => !Equals(s.Id, x.Id)))
			//	.ToList();

			//var removeStudents = dbGrade
			//	.Students
			//	.Where(x => editGrade.Students.All(s => !Equals(s.Id, x.Id)))
			//	.ToList();

			//if (addStudents.Any())
			//{
			//	dbGrade.Students.AddRange(addStudents);
			//}

			//if (removeStudents.Any())
			//{
			//	foreach (var removeStudent in removeStudents)
			//	{
			//		dbGrade.Students.Remove(removeStudent);
			//	}
			//}

			//DataContext.Update(dbGrade);

			//await DataContext.SaveChangesAsync();

			return editGrade;
		}

		public async Task<Grade> AddNewGradeAsync(Grade grade)
		{
			var dbGrade = await GetGradeByIdAsync(grade.Id);

			if (dbGrade == null)
			{
				DataContext.Grades.Add(grade);
			}

			await DataContext.SaveChangesAsync();
			return grade;
		}

		public Task<Grade> AddNewGradeAsync(GradeViewModel gradeViewModel)
		{
			var grade = gradeViewModel.Grade;

			var students = _studentRepository.GetStudents();

			var selectedStudentIds = gradeViewModel.StudentIds;

			var selectedStudents = students.Where(s => selectedStudentIds.Contains(s.Id));

			grade.Students = selectedStudents.ToList();

			return AddNewGradeAsync(grade);
		}

	}
}
