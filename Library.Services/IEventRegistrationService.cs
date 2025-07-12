using Library.Models;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IEventRegistrationService
    {
        bool RegisterUser(int eventId, string userId, out string message);
        bool CancelRegistration(int eventId, string userId, out string message);
        bool MarkAsParticipated(int eventParticipantId);
        PagedResult<EventParticipant> GetUserRegistrations(string userId, int pageNumber, int pageSize);
    }
}
