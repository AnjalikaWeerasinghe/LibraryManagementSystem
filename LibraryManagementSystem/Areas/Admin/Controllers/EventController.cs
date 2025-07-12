using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _webHostEnvironment;

        public EventController(ILibraryEventService libraryEvent, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _libraryEvent = libraryEvent;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Edit(LibraryEventViewModel vm, IFormFile ImageFile)
        {
            // If validation fails, return early
            if (!ModelState.IsValid)
                return View(vm);

            // Retrieve existing entity
            var libevent = _unitOfWork.GenericRepository<LibraryEvent>().GetById(vm.Id);
            if (libevent == null)
                return NotFound();

            // If a new image is uploaded, save it and update ImageUrl
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string imagePath = Path.Combine(wwwRootPath, "Images/events");

                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                string fullPath = Path.Combine(imagePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                // Update ImageUrl
                libevent.ImageUrl = "/Images/events/" + fileName;
                vm.ImageUrl = libevent.ImageUrl; // sync ViewModel too
            }

            // Update remaining fields
            libevent.EventCode = vm.EventCode;
            libevent.Title = vm.Title;
            libevent.Description = vm.Description;
            libevent.StartDate = vm.StartDate;
            libevent.EndDate = vm.EndDate;
            libevent.Location = vm.Location;

            // Save to DB
            _unitOfWork.GenericRepository<LibraryEvent>().Update(libevent);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $"'{libevent.Title}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new LibraryEventViewModel
            {
                EventCode = _libraryEvent.GenerateNextEventCode()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LibraryEventViewModel vm, IFormFile ImageFile)
        {
            // Auto-generate EventCode BEFORE model validation
            vm.EventCode = _libraryEvent.GenerateNextEventCode();

            if (!ModelState.IsValid)
            {
                return View(vm); // Return validation errors
            }

            // Handle image upload (if applicable)
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string imagePath = Path.Combine(wwwRootPath, "images/events");

                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                string fullPath = Path.Combine(imagePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                // Set the image URL in view model
                vm.ImageUrl = "/images/events/" + fileName;
            }

            try
            {
                // Map ViewModel to Entity
                var entity = new LibraryEvent
                {
                    EventCode = vm.EventCode,
                    Title = vm.Title,
                    Description = vm.Description,
                    ImageUrl = vm.ImageUrl,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    Location = vm.Location
                };

                _unitOfWork.GenericRepository<LibraryEvent>().Add(entity);
                _unitOfWork.Save();

                TempData["SuccessMessage"] = $"{vm.Title} successfully added!";

                // Prepare a new ViewModel with a fresh EventCode
                var newVm = new LibraryEventViewModel
                {
                    EventCode = _libraryEvent.GenerateNextEventCode()
                };

                ModelState.Clear(); // Reset form validation
                return View(newVm);
            }
            catch (Exception ex)
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
