using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member,Admin")]
    public class EventParticipantController : Controller
    {
        private readonly IEventRegistrationService _eventRegistrationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public EventParticipantController (IEventRegistrationService eventRegistrationService, UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _eventRegistrationService = eventRegistrationService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterForm(int eventId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var libraryEvent = await _unitOfWork.GenericRepository<LibraryEvent>().GetByIdAsync(eventId);
            if (libraryEvent == null)
                return NotFound("Event not found");

            var vm = new EventParticipantViewModel
            {
                LibraryEventId = eventId,
                StartDate = libraryEvent.StartDate,
                ApplicationUserId = user.Id,
                Location = libraryEvent.Location ,
                EventTitle = libraryEvent.Title
            };

            return PartialView("_RegisterForm", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(EventParticipantViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventRegistrationService.RegisterAsync(vm.LibraryEventId, vm.ApplicationUserId);
            TempData["SuccessMessage"] = result;
            //return RedirectToAction("Details", "Event", new { id = vm.LibraryEventId });

            return RedirectToAction("Index", "Event", new { id = vm.LibraryEventId });

        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int registrationId, int eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _eventRegistrationService.CancelRegistrationAsync(registrationId, userId);
            TempData["Message"] = "Registration cancelled.";

            return RedirectToAction("Details", "LibraryEvent", new { id = eventId });
        }

        public async Task<IActionResult> MyRegistrations()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myRegistrations = await _eventRegistrationService.GetRegistrationsByUserAsync(userId);
            return View(myRegistrations);
        }
    }
}
