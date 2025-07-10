using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BookController : Controller
    {
        private readonly IBookService _book;
        private IUnitOfWork _unitOfWork;

        public BookController(IBookService book, IUnitOfWork unitOfWork)
        {
            _book = book;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_book.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = _book.GetBookById(id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var book = _unitOfWork.GenericRepository<Book>().GetById(vm.Id);
            if (book == null)
                return NotFound();

            book.Title = vm.Title;
            book.Description = vm.Description;
            book.ISBN = vm.ISBN;
            book.ItemCode = vm.ItemCode;
            book.CategoryId = vm.CategoryId;
            book.YearPublished = vm.YearPublished;
            book.PublisherId = vm.PublisherId;
            book.LanguageId = vm.LanguageId;
            book.GenreId = vm.GenreId;
            book.ShelfLocation = vm.ShelfLocation;
            book.Edition = vm.Edition;

            _unitOfWork.GenericRepository<Book>().Update(book);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{book.Title}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel vm)
        {
            _book.InsertBook(vm);
            TempData["SuccessMessage"] = $"{vm.Title} successfully added!";

            return View(new BookViewModel());

        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<Book>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Book>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
