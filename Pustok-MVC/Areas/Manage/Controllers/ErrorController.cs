using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pustok_MVC.Areas.Manage.Controllers
{
    public class ErrorController : Controller
    {
        [Authorize]
        [Area("manage")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
