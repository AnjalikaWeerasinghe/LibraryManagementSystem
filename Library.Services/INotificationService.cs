using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface INotificationService
    {
        Task<List<NotificationViewModel>> GetUserNotificationsAsync(string userId); // For member
    }
}
