using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Member.Controllers
{
    [Area("Member")]
    public class NotificationController : Controller
    {

        private readonly INotificationService _notificationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationController(INotificationService notificationService, UserManager<ApplicationUser> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNotifications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var notifications = await _notificationService.GetUserNotificationsAsync(user.Id);
            return PartialView("_NotificationDropdown", notifications);
        }
    }
}
