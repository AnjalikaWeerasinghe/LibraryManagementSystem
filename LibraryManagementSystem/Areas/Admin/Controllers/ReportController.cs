using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Encodings.Web;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _service;
        private readonly IUnitOfWork _unitOfWork;
        public ReportController(IReportService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _service.GetCountsAsync();

            var chartData = vm
                .GroupBy(x => new { x.ItemType, x.Category })
                .Select(g => new
                {
                    label = $"{g.Key.ItemType} - {g.Key.Category}",
                    value = g.Sum(r => r.Count)
                })
                .OrderByDescending(x => x.value)
                .ToList();

            ViewBag.ChartJson = Newtonsoft.Json.JsonConvert.SerializeObject(chartData);

            return View(vm);
        }

        public async Task<IActionResult> MemberRegistrationReport()
        {
            var result = await _service.GetMemberRegistrationReportAsync();

            ViewBag.ChartJson = result.ChartJson;
            return View(result);
        }


    }
}
