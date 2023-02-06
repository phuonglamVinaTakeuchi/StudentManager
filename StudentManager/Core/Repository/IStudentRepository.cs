using StudentManager.Core.Data;

namespace StudentManager.Core.Repository
{
	public interface IStudentRepository
	{
		IEnumerable<Student> GetStudents();
		Task<Student?> GetStudentByIdAsync(string studentId);
		//Task<Grade> UpdateGradeAsync(GradeViewModel student);
		//Task<Grade> UpdateGradeAsync(Grade student);
		Task<Student> AddNewStudentAsync(Student student);
	}
}
