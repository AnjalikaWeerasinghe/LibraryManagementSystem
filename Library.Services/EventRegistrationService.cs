using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
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

        public async Task CancelRegistrationAsync(int eventregistrationId, string userId)
        {
            var registration = _unitOfWork.GenericRepository<EventParticipant>()
            .GetAll()
            .FirstOrDefault(p => p.Id == eventregistrationId && p.ApplicationUserId == userId);

            if (registration == null)
                throw new Exception("Registration not found.");

            _unitOfWork.GenericRepository<EventParticipant>().Delete(registration);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<EventParticipationAdminViewModel>> GetEventParticipationReportAsync()
        {
            var participants = _unitOfWork.GenericRepository<EventParticipant>()
            .GetAll(includeProperties: "LibraryEvent,ApplicationUser")
            .ToList();

            var grouped = participants
                .GroupBy(p => new
                {
                    p.LibraryEvent.Id,
                    p.LibraryEvent.Title,
                    p.LibraryEvent.StartDate,
                    p.LibraryEvent.Location
                })
                .Select(g => new EventParticipationAdminViewModel
                {
                    EventTitle = g.Key.Title,
                    StartDate = g.Key.StartDate,
                    Location = g.Key.Location,
                    Participants = g.Select(p => new EventParticipationAdminViewModel.ParticipantDetail
                    {
                        FullName = p.ApplicationUser.FullName,
                        UserCode = p.ApplicationUser.UserCode,
                        Status = p.ParticipantStatus
                    }).ToList()
                }).ToList();

            return await Task.FromResult(grouped);
        }

        public async Task<IEnumerable<EventParticipantViewModel>> GetRegistrationsByEventAsync(int eventId)
        {
            var participants = _unitOfWork.GenericRepository<EventParticipant>()
            .GetAll(includeProperties: "LibraryEvent,ApplicationUser")
            .Where(p => p.LibraryEventId == eventId)
            .ToList();

            var vms = participants.Select(p => new EventParticipantViewModel(p));
            return await Task.FromResult(vms);
        }

        public async Task<IEnumerable<EventParticipantViewModel>> GetRegistrationsByUserAsync(string userId)
        {
            var registrations = _unitOfWork.GenericRepository<EventParticipant>()
            .GetAll(includeProperties: "LibraryEvent,ApplicationUser")
            .Where(p => p.ApplicationUserId == userId)
            .ToList();

            var viewModels = registrations.Select(r => new EventParticipantViewModel(r));
            return await Task.FromResult(viewModels);
        }

        public async Task<string> RegisterAsync(int eventId, string userId)
        {
            var existing = _unitOfWork.GenericRepository<EventParticipant>()
                .GetAll(includeProperties: "LibraryEvent,ApplicationUser")
                .FirstOrDefault(p => p.LibraryEventId == eventId && p.ApplicationUserId == userId);

            var ev = _unitOfWork.GenericRepository<LibraryEvent>().GetById(eventId);

            if (existing != null)
                return "Already registered.";

            var registration = new EventParticipant
            {
                LibraryEventId = eventId,
                ApplicationUserId = userId,
                StartDate = ev.StartDate,
                ParticipantStatus = ParticipantStatus.Registered,
                Location = ev.Location,
                EventTitle = ev.Title
            };

            _unitOfWork.GenericRepository<EventParticipant>().Add(registration);
            await _unitOfWork.SaveAsync();

            return "Registration Successful.";
        }


    }
}
