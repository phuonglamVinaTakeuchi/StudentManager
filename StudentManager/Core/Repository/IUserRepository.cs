using Microsoft.AspNetCore.Identity;
using StudentManager.Areas.Identity.Data;
using StudentManager.Models;

namespace StudentManager.Core.Repository
{
	public interface IUserRepository : IRepository
	{
		ICollection<AppUser> GetUsers();

		AppUser? GetUserById(string id);

		AppUser? UpdateUser(AppUser user);

		Task<AppUser?> UpdateUser(EditUserViewModel user);

		Task<IList<string>> GetUserRoles(AppUser user);
	}
}
