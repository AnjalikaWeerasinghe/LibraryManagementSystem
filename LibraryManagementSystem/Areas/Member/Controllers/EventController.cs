using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("member")]
    public class EventController : Controller
    {
        private ILibraryEventService _libraryEvent;
        private IUnitOfWork _unitOfWork;

        public EventController(ILibraryEventService libraryEvent, IUnitOfWork unitOfWork)
        {
            _libraryEvent = libraryEvent;
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_libraryEvent.GetAll(pageNumber, pageSize));
        }

        public IActionResult Details(int id)
        {
            var evt = _unitOfWork.GenericRepository<LibraryEvent>().GetById(id);
            if (evt == null) return NotFound();

            var vm = new LibraryEventViewModel
            {
                Id = evt.Id,
                Title = evt.Title,
                Description = evt.Description,
                ImageUrl = evt.ImageUrl,
                StartDate = evt.StartDate
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Register(int eventId)
        {
            // Pass the eventId to the view or fetch event details if needed
            var model = new EventParticipantViewModel
            {
                LibraryEventId = eventId
                // Optionally load Event title, date etc.
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(EventParticipantViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Save registration logic here
            // Redirect or show success

            return RedirectToAction("Confirmation");
        }
    }
}
