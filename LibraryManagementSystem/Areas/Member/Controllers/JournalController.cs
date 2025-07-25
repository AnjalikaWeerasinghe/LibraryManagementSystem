using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("Member")]
    public class JournalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJournalService _journalService;

        public JournalController(IUnitOfWork unitOfWork, IJournalService journalService)
        {
            _unitOfWork = unitOfWork;
            _journalService = journalService;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<JournalViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _journalService.GetJournalByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _journalService.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }
    }
}
