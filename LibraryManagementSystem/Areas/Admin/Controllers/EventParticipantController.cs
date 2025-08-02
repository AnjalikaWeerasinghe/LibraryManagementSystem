using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventParticipantController : Controller
    {
        private readonly IEventRegistrationService _eventRegistrationService;

        public EventParticipantController(IEventRegistrationService eventRegistrationService)
        {
            _eventRegistrationService = eventRegistrationService;
        }

        public async Task<IActionResult> RegistrationsForEvent(int eventId)
        {
            var registrations = await _eventRegistrationService.GetRegistrationsByEventAsync(eventId);
            return View(registrations);
        }

        public async Task<IActionResult> EventParticipation()
        {
            var report = await _eventRegistrationService.GetEventParticipationReportAsync();
            return View(report);
        }
    }
}
