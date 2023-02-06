using StudentManager.Core.Data;

namespace StudentManager.Models
{
  public class StudentViewModel
  {
	  public Student Student { get; set; }
	  public IEnumerable<Grade> Grades { get; set; }
  }
}
