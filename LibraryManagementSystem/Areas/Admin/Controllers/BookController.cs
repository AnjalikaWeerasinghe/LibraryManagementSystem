using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Services.Results;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private IBookService _book;
        private IUnitOfWork _unitOfWork;
        private ILanguageService _languageService;
        private IPublisherService _publisherService;
        private ICategoryService _categoryService;
        private IGenreService _genreService;

        public BookController(IBookService book, IUnitOfWork unitOfWork, 
            ILanguageService languageService, IPublisherService publisherService, ICategoryService categoryService, 
            IGenreService genreService)
        {
            _book = book;
            _unitOfWork = unitOfWork;
            _languageService = languageService;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _genreService = genreService;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<BookViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _book.GetBookByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _book.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = _book.GetBookById(id);
            if (book == null) return NotFound();

            var vm = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedYear = book.PublishedYear,
                LanguageId = book.LanguageId,
                CategoryId = book.CategoryId,
                PublisherId = book.PublisherId,
                Description = book.Description,
                Edition = book.Edition,
                GenreId = book.GenreId,
                ItemCode = book.ItemCode,

                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = (await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Book).ToList(),
                Genres = await _genreService.GetAllAsync(),

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = await _languageService.GetAllAsync();
                vm.Publishers = await _publisherService.GetAllAsync();
                vm.Categories = (await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Book).ToList();
                vm.Genres = await _genreService.GetAllAsync();

            }

            var book = _unitOfWork.GenericRepository<Book>().GetById(vm.Id);
            if (book == null)
                return NotFound();

            book.Id = vm.Id; 
            book.Title = vm.Title;
            book.Description = vm.Description;
            book.ISBN = vm.ISBN;
            book.ItemCode = vm.ItemCode;
            book.CategoryId = vm.CategoryId;
            book.PublishedYear = vm.PublishedYear;
            book.PublisherId = vm.PublisherId;
            book.LanguageId = vm.LanguageId;
            book.GenreId = vm.GenreId;
            book.Edition = vm.Edition;

            _unitOfWork.GenericRepository<Book>().Update(book);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = $"{vm.Title} updated successfully !";

            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var vm = new BookViewModel
            {
                ItemCode = _book.GenerateNextBookCode(),
                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Genres = await _genreService.GetAllAsync()

            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
                
            }

            vm.ItemCode = _book.GenerateNextBookCode();

            InsertBookResult result = _book.InsertBook(vm);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            else
            {
                TempData["SuccessMessage"] = $"{vm.Title} successfully added!";
            }

            return RedirectToAction(nameof(Create));

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

        private async Task PopulateDropdowns(BookViewModel vm)
        {
            vm.Languages = await _languageService.GetAllAsync();
            vm.Publishers = await _publisherService.GetAllAsync();
            vm.Categories = await _categoryService.GetAllAsync();
            vm.Genres = await _genreService.GetAllAsync();
        }
    }
}
