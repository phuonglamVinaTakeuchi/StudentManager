using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManager.Areas.Identity.Data;

namespace StudentManager.Models
{
	public class EditUserViewModel
	{
		public AppUser User { get; set; }

		public IList<SelectListItem> Roles { get; set; }
	}
}
