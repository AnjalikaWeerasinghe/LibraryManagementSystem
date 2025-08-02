using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing.Printing;
using System.Security.Policy;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> Edit(int id)
        {
            var libevent = _libraryEvent.GetLibraryEventById(id);
            if (libevent == null) return NotFound();

            var vm = new LibraryEventViewModel
            {
                Id = libevent.Id,
                Title = libevent.Title,
                EventCode = libevent.EventCode,
                Description = libevent.Description,
                ImageUrl = libevent.ImageUrl,
                StartDate = libevent.StartDate,
                EndDate = libevent.EndDate,
                Location = libevent.Location,
            };
            return View(vm);

        }

        [HttpPost]
        public IActionResult Edit(LibraryEventViewModel vm, IFormFile ImageFile)
        {
           
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
            libevent.Id = vm.Id;
            libevent.EventCode = vm.EventCode;
            libevent.Title = vm.Title;
            libevent.Description = vm.Description;
            libevent.StartDate = vm.StartDate;
            libevent.EndDate = vm.EndDate;
            libevent.Location = vm.Location;

            // Save to DB
            _unitOfWork.GenericRepository<LibraryEvent>().Update(libevent);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = $"{vm.Title} updated successfully !";

            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new LibraryEventViewModel
            {
                EventCode = _libraryEvent.GenerateNextEventCode(),

                //EventStatus = Enum.GetValues(typeof(UserStatus))
                //    .Cast<UserStatus>()
                //    .Select(e => new SelectListItem
                //    {
                //        Value = e.ToString(),
                //        Text = e.ToString()
                //    }),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(LibraryEventViewModel vm, IFormFile ImageFile)
        {
            
            vm.EventCode = _libraryEvent.GenerateNextEventCode();

            // Handle image upload 
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

            var result = _libraryEvent.InsertLibraryEvent(vm);
            if (result == "Event already exists.")
            {
                TempData["ErrorMessage"] = result;
            }
            else
            {
                TempData["SuccessMessage"] = $"{vm.Title} successfully added!";
            }

            return RedirectToAction(nameof(Create));

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

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            try
            {
                 _libraryEvent.ToggleEventStatusAsync(id);
                TempData["SuccessMessage"] = "Event status updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
