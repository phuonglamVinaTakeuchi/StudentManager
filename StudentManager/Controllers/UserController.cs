using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManager.Core.Repository;
using StudentManager.Models;

namespace StudentManager.Controllers
{
	public class UserController : Controller
	{
		private readonly IRepositories _repositories;
		public UserController(IRepositories repositories)
		{
			_repositories = repositories;
		}
		public Task<IActionResult> Index()
		{
			var users = _repositories.UserRepository.GetUsers();
			return Task.FromResult<IActionResult>(View(users));
		}
		public async Task<IActionResult> Edit(string id)
		{
			var user = _repositories.UserRepository.GetUserById(id);

			if (user == null)
			{
				return NotFound();
			}

			var roles = _repositories.RoleRepository.GetRoles();

			var userRoles = await _repositories.UserRepository.GetUserRoles(user);

			var selectedItems = roles.Select(r => new SelectListItem(r.Name,r.Id,userRoles.Any(ur=>Equals(ur,r.Name))));

			var editUserViewModel = new EditUserViewModel()
			{
				User = user,
				Roles = selectedItems.ToList()
			};

			return View(editUserViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel userEdit)
		{
			var user = await _repositories.UserRepository.UpdateUser(userEdit);
			if (user == null)
			{
				return NotFound();
			}

			return RedirectToAction("Edit");
		}
	}
}
