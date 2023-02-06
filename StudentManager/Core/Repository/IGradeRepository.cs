using StudentManager.Areas.Identity.Data;
using StudentManager.Core.Data;
using StudentManager.Models;

namespace StudentManager.Core.Repository
{
	public interface IGradeRepository
	{
		IEnumerable<Grade> GetGrades();
		Task<Grade?> GetGradeByIdAsync(string gradeId);

		Task<Grade> UpdateGradeAsync(Grade grade);
		Task<Grade> UpdateGradeAsync(GradeViewModel grade);
		Task<Grade> AddNewGradeAsync(Grade grade);
		Task<Grade> AddNewGradeAsync(GradeViewModel gradeViewModel);
		//Task<Grade> AddNewGradesAsync(IEnumerable<Grade> grades);

		//Task<Grade> AddNewStudent(AppUser user);
		//Task<Grade> AddNewStudents(IEnumerable<AppUser> users);

		//Task<Grade> RemoveGrade(string gradeId);
		//Task<Grade> RemoveStudentFromGrade(AppUser user);
		//Task<Grade> RemoveStudentsFromGrade(IEnumerable<AppUser> users);
		//Task<Grade> RemoveStudentFromGrade(string studentId);
	}
}
