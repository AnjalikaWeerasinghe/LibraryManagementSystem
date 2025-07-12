using Library.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _service.GetCountsAsync();

            var chartData = vm
                .GroupBy(x => x.Category)
                .Select(g => new { label = g.Key, value = g.Sum(r => r.Count) })
                .OrderByDescending(x => x.value)
                .ToList();

            ViewBag.ChartJson = JsonSerializer.Serialize(chartData);

            return View(vm);
        }
    }
}
