using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Core;

namespace StudentManager.Controllers
{
  public class RoleController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public RoleController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    //[Authorize(Policy = "EmployeeOnly")]
    public IActionResult Index()
    {
	    return View();
    }

    [Authorize(Policy = Constants.Policies.RequireManager)]
    public IActionResult Manager()
    {
      return View();
    }

    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public IActionResult Admin()
    {
      return View();
    }
  }
}
