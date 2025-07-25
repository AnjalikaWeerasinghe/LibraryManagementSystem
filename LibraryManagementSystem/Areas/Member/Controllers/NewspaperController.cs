using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("Member")]
    public class NewspaperController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewspaperService _newspaperService;

        public NewspaperController(IUnitOfWork unitOfWork, INewspaperService newspaperService)
        {
            _unitOfWork = unitOfWork;
            _newspaperService = newspaperService;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<NewspaperViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _newspaperService.GetNewspaperByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _newspaperService.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }
    }
}
