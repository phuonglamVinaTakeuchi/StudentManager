using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManager.Core.Data;

namespace StudentManager.Models
{
	public class GradeViewModel
	{
		public Grade Grade { get; set; }

		public IList<SelectListItem> Students { get; set; }

		[BindProperty]
		public IList<string> StudentIds { get; set; }
	}
}
