using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("Member")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookService _bookService;

        public BookController(IUnitOfWork unitOfWork, IBookService bookService)
        {
            _unitOfWork = unitOfWork;
            _bookService = bookService;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<BookViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _bookService.GetBookByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _bookService.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }
    }
}
