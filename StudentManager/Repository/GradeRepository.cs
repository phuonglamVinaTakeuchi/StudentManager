using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
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
			return await DataContext.Grades.Include(x=>x.Students).FirstOrDefaultAsync(x => Equals(x.Id, gradeId));
		}

		public async Task<Grade> UpdateGradeAsync(GradeViewModel editGrade)
		{
			var students = _studentRepository.GetStudents();

			var selectedStudentIds = editGrade.StudentIds;

			var selectedStudents = students.Where(s => selectedStudentIds.Contains(s.Id));
			
			editGrade.Grade.Students.Clear();
			editGrade.Grade.Students.AddRange(selectedStudents);

			return await UpdateGradeAsync(editGrade.Grade);
		}

		public async Task<Grade> UpdateGradeAsync(Grade editGrade)
		{
			var dbGrade = await GetGradeByIdAsync(editGrade.Id);

			if (dbGrade == null)
			{
				DataContext.Add(editGrade);
				await DataContext.SaveChangesAsync();
				return editGrade;
			}

			dbGrade.Name = editGrade.Name;
			dbGrade.Description = editGrade.Description;

			dbGrade.Students.Clear();
			dbGrade.Students.AddRange(editGrade.Students);

			DataContext.Grades.Attach(dbGrade);
			DataContext.Entry(dbGrade).State = EntityState.Modified;

			await DataContext.SaveChangesAsync();

			return dbGrade;
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
