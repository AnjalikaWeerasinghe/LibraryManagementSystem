using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("member")]
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
