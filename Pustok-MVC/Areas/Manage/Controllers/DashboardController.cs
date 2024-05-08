using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pustok_MVC.Areas.Manage.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        [Area("manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
