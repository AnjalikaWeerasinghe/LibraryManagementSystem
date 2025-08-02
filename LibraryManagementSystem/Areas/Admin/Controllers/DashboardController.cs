using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IAdminDashboardService _dashboardService;

        public DashboardController(IAdminDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dashboardService.GetDashboardDataAsync();
            return View(model);
        }
    }
}
