using Microsoft.AspNetCore.Identity;
using StudentManager.Areas.Identity.Data;
using StudentManager.Core.Repository;

namespace StudentManager.Repository
{
	public class RoleRepository : RepositoryBase,IRoleRepository
	{
		public RoleRepository(StudentManagerContext dataContext) : base(dataContext)
		{
		}

		public ICollection<IdentityRole> GetRoles()
		{
			return DataContext.Roles.ToList();
		}
	}
}
