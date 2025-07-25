using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("Member")]
    public class PeriodicalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPeriodicalService _periodicalService;

        public PeriodicalController(IUnitOfWork unitOfWork, IPeriodicalService periodicalService)
        {
            _unitOfWork = unitOfWork;
            _periodicalService = periodicalService;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {

            PagedResult<PeriodicalViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _periodicalService.GetPeriodicalByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _periodicalService.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }
    }
}
