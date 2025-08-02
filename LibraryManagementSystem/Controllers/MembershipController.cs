using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class MembershipController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}
