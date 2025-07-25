using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LibraryController : Controller
    {
        private ILibraryInfoService _libraryInfo;

        public LibraryController(ILibraryInfoService libraryInfo)
        {
            _libraryInfo = libraryInfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_libraryInfo.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var viewModel = _libraryInfo.GetLibraryById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(LibraryInfoViewModel vm)
        {
            _libraryInfo.UpdateLibraryInfo(vm);
            return RedirectToAction("Index");
        }

    }
}
