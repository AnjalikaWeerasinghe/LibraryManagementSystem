using Library.Models;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EventController : Controller
    {
        private ILibraryEventService _libraryEvent;

        public EventController(ILibraryEventService libraryEvent)
        {
            _libraryEvent = libraryEvent;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_libraryEvent.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _libraryEvent.GetLibraryEventById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(LibraryEventViewModel vm)
        {
            _libraryEvent.UpdateLibraryEvent(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LibraryEventViewModel vm)
        {
            _libraryEvent.InsertLibraryEvent(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _libraryEvent.DeleteLibraryEvent(id);
            return RedirectToAction("Index");
        }
    }
}
