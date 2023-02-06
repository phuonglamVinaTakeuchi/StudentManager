using Microsoft.AspNetCore.Identity;
using StudentManager.Core.Data;

namespace StudentManager.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }

  public virtual IEnumerable<Grade> Grades { get; set; }
}

