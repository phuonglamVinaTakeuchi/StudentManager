using System.ComponentModel.DataAnnotations;

namespace StudentManager.Core.Data
{
	public class Student
	{
		public string Id { get; set; }
		[Required,MaxLength(255)]
		public string Name { get; set; }

		public string Email { get; set; }

		public string Address { get; set; }

		public virtual ICollection<Grade> Grades { get; set; }
	}
}
