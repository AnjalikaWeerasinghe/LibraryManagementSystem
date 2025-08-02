using Library.Models;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Areas.Identity.Pages.Account.Manage
{
    public class MyEventModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventRegistrationService _eventService;

        public MyEventModel(UserManager<ApplicationUser> userManager, IEventRegistrationService eventService)
        {
            _userManager = userManager;
            _eventService = eventService;
        }

        public List<EventParticipantViewModel> RegisteredEvents { get; set; }

        public int eventId { get; set; }

        public string userId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //this.eventId = eventId;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found");

            RegisteredEvents = (await _eventService.GetRegistrationsByUserAsync(user.Id)).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostCancelRegistrationAsync(int eventregistrationId)
        {
            var userId = _userManager.GetUserId(User);
            await _eventService.CancelRegistrationAsync(eventregistrationId, userId);

            return RedirectToPage();
        }
    }
}
