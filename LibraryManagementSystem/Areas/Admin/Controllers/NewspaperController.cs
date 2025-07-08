using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    public class NewspaperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
