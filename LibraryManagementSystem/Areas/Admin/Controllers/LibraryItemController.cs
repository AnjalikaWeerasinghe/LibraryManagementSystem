using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    public class LibraryItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
