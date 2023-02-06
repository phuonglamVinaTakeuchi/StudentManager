using Microsoft.AspNetCore.Identity;
using StudentManager.Areas.Identity.Data;
using StudentManager.Core.Repository;
using StudentManager.Models;

namespace StudentManager.Repository
{
	public class UserRepository : RepositoryBase, IUserRepository
	{
		private SignInManager<AppUser> _signInManager;
		public UserRepository(StudentManagerContext dbContext, SignInManager<AppUser> signInManager) : base(dbContext)
		{
			_signInManager = signInManager;
		}
		public ICollection<AppUser> GetUsers()
		{
			return DataContext.Users.ToList();
		}

		public AppUser? GetUserById(string id)
		{
			return DataContext.Users.FirstOrDefault(u => Equals(u.Id, id));
		}

		public AppUser? UpdateUser(AppUser user)
		{
			DataContext.Update(user);
			DataContext.SaveChanges();
			return user;
		}

		public async Task<AppUser?> UpdateUser(EditUserViewModel userEdit)
		{
			var user = GetUserById(userEdit.User.Id);
			if (user == null)
			{
				return null;
			}

			user.FirstName = userEdit.User.FirstName;
			user.LastName = userEdit.User.LastName;
			user.Email = userEdit.User.Email;

			var userRoles = GetUserRoles(user).Result;

			var addRoles = userEdit
				.Roles
				.Where(x => 
					x.Selected &&
					!userRoles.Contains(x.Text))
				.Select(r=>r.Text)
				.ToList();

			var removeRoles = userEdit
				.Roles
				.Where(x=>!x.Selected && 
				          userRoles.Contains(x.Text))
				.Select(r=>r.Text)
				.ToList();

			if (addRoles.Any()) await _signInManager.UserManager.AddToRolesAsync(user, addRoles);
			if (removeRoles.Any()) await _signInManager.UserManager.RemoveFromRolesAsync(user, removeRoles);

			UpdateUser(user);

			return user;
		}

		public async Task<IList<string>> GetUserRoles(AppUser user)
		{
			return await _signInManager.UserManager.GetRolesAsync(user);
		}
	}
}
