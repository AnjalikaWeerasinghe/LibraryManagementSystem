using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private IUnitOfWork _unitOfWork;

        public EventRegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CancelRegistration(int eventId, string userId, out string message)
        {
            throw new NotImplementedException();
        }

        public PagedResult<EventParticipant> GetUserRegistrations(string userId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public bool MarkAsParticipated(int eventParticipantId)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(int eventId, string userId, out string message)
        {
            // Check for duplicate
            var exists = _unitOfWork.GenericRepository<EventParticipant>()
                             .GetAll(ep => ep.LibraryEventId == eventId &&
                                           ep.ApplicationUserId == userId)
                             .Any();
            if (exists)
            {
                message = "You are already registered for this event.";
                return false;
            }

            // Save registration
            _unitOfWork.GenericRepository<EventParticipant>().Add(new EventParticipant
            {
                LibraryEventId = eventId,
                ApplicationUserId = userId
            });
            _unitOfWork.Save();
            message = "Registration successful!";
            return true;
        }
    }
}
