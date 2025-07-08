using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    public class PeriodicalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
