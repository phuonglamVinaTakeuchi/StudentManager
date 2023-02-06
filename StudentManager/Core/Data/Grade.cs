using System.ComponentModel.DataAnnotations;

namespace StudentManager.Core.Data
{
	public class Grade
	{
		public string Id { get; set; }

		[Required,MaxLength(255)]
		public string Name { get; set; }

		public string Description { get; set; }
		public virtual ICollection<Student> Students { get; set; }
	}
}
