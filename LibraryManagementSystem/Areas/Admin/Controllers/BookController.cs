using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BookController : Controller
    {
        private readonly ILibraryItemService _libraryitem;
        private IUnitOfWork _unitOfWork;

        public BookController(ILibraryItemService libraryitem, IUnitOfWork unitOfWork)
        {
            _libraryitem = libraryitem;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_libraryitem.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bookitem = await _libraryitem.GetLibraryItemByIdAsync(id);
            if (bookitem == null || bookitem is not BookViewModel)
            {
                return NotFound();
            }
            return View(bookitem as BookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _libraryitem.UpdateLibraryItemAsync(vm);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Error Occured While Updating the Book.");
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new BookViewModel
            {
                CategoryList = _unitOfWork.GenericRepository<Category>()
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _libraryitem.InsertLibraryItem(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error occured while saving the Book.");
                return View(vm);
            }
            
        }

        public IActionResult Delete(int id)
        {
            _libraryitem.DeleteLibraryItem(id);
            return RedirectToAction("Index");
        }
    }
}
