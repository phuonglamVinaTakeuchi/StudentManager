using Microsoft.AspNetCore.Identity;

namespace StudentManager.Core.Repository
{
	public interface IRoleRepository: IRepository
	{
		ICollection<IdentityRole> GetRoles();
	}
}
