using Library.Models;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IEventRegistrationService
    {
        Task<string> RegisterAsync(int eventId, string userId);
        Task CancelRegistrationAsync(int eventregistrationId, string userId);
        Task<IEnumerable<EventParticipantViewModel>> GetRegistrationsByEventAsync(int eventId);
        Task<IEnumerable<EventParticipantViewModel>> GetRegistrationsByUserAsync(string userId);

        Task<List<EventParticipationAdminViewModel>> GetEventParticipationReportAsync();

    }
}
