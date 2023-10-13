using Microsoft.AspNetCore.Mvc;

namespace startup.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
