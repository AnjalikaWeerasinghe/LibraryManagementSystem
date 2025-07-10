using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Security.Policy;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EventController : Controller
    {
        private ILibraryEventService _libraryEvent;
        private IUnitOfWork _unitOfWork;

        public EventController(ILibraryEventService libraryEvent, IUnitOfWork unitOfWork)
        {
            _libraryEvent = libraryEvent;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<LibraryEventViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _libraryEvent.GetEventByTitle(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _libraryEvent.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
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
            if (!ModelState.IsValid)
                return View(vm);

            var libevent = _unitOfWork.GenericRepository<LibraryEvent>().GetById(vm.Id);
            if (libevent == null)
                return NotFound();

            libevent.EventCode = vm.EventCode;
            libevent.Title = vm.Title;
            libevent.Description = vm.Description;
            libevent.ImageUrl = vm.ImageUrl;
            libevent.StartDate = vm.StartDate;
            libevent.EndDate = vm.EndDate;
            libevent.Location = vm.Location;
            libevent.CreatedBy = vm.CreatedBy;

            _unitOfWork.GenericRepository<LibraryEvent>().Update(libevent);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{libevent.Title}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LibraryEventViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _unitOfWork.GenericRepository<LibraryEvent>().Add(new LibraryEvent
                {
                    EventCode = vm.EventCode,
                    Title = vm.Title,
                    Description = vm.Description,
                    ImageUrl = vm.ImageUrl,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    Location = vm.Location,
                    CreatedBy = vm.CreatedBy
                });
                _unitOfWork.Save();

                TempData["SuccessMessage"] = $"{vm.Title} successfully Added !";
                ModelState.Clear(); // clear input fields
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while saving.");
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<LibraryEvent>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<LibraryEvent>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
