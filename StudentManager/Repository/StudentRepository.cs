using Microsoft.EntityFrameworkCore;
using StudentManager.Areas.Identity.Data;
using StudentManager.Core.Data;
using StudentManager.Core.Repository;
using StudentManager.Models;

namespace StudentManager.Repository
{
	public class StudentRepository : RepositoryBase, IStudentRepository
	{
    public StudentRepository(
			StudentManagerContext dataContext) : base(dataContext)
		{
			
		}
		public IEnumerable<Student> GetStudents()
		{
			return DataContext.Students.Include(x=>x.Grades).ToList();
		}

		public async Task<Student?> GetStudentByIdAsync(string studentId)
		{
			return await DataContext.Students.FirstOrDefaultAsync(x => Equals(x.Id, studentId));
		}

		public async Task<Student> AddNewStudentAsync(Student student)
		{
			var dbStudent = await GetStudentByIdAsync(student.Id);

			if (dbStudent == null)
			{
				DataContext.Students.Add(student);
			}

			await DataContext.SaveChangesAsync();
			return student;
		}

		//public async Task<Student> AddNewStudentAsync(StudentViewModel student)
		//{
		//	var dbStudent = await GetStudentByIdAsync(student.Student.Id);

		//	if (dbStudent == null)
		//	{
		//		DataContext.Students.Add(student);
		//	}

		//	await DataContext.SaveChangesAsync();
		//	return student;
		//}
	}
}
