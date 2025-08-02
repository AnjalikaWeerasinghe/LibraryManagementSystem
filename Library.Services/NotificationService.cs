using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<NotificationViewModel>> GetUserNotificationsAsync(string userId)
        {
            var notifications = new List<NotificationViewModel>();

            // Notifications for Library Events
            var eventNotifications = _unitOfWork.GenericRepository<EventParticipant>()
                .GetAll(includeProperties: "LibraryEvent")
                .Where(e => e.ApplicationUserId == userId && e.ParticipantStatus == ParticipantStatus.Registered && e.LibraryEvent.StartDate >= DateTime.Today)
                .ToList();

            foreach (var ev in eventNotifications)
            {
                notifications.Add(new NotificationViewModel
                {
                    Message = $"You have an upcoming event - \"{ev.LibraryEvent.Title}\"",
                    Date = ev.LibraryEvent.StartDate,
                    Type = "Event",
                });
            }

            return notifications.OrderBy(n => n.Date).ToList();
        }
    }
}
