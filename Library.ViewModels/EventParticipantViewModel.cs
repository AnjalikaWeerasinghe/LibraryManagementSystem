using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class EventParticipantViewModel
    {
        public int Id { get; set; }
        public int LibraryEventId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ParticipantStatus ParticipantStatus { get; set; }
        public string ApplicationUserId { get; set; } //Foreign key to Application User

        public EventParticipantViewModel()
        {
            
        }

        public EventParticipantViewModel(EventParticipant participant)
        {
            Id = participant.Id;
            LibraryEventId = participant.LibraryEventId;
            RegisteredDate = participant.RegisteredDate;
            ParticipantStatus = participant.ParticipantStatus;
            ApplicationUserId = participant.ApplicationUserId;
        }

        public EventParticipant ConvertViewModelToModel(EventParticipantViewModel model)
        {
            return new EventParticipant
            {
                Id = model.Id,
                LibraryEventId = model.LibraryEventId,
                RegisteredDate = model.RegisteredDate,
                ParticipantStatus = model.ParticipantStatus,
                ApplicationUserId = model.ApplicationUserId
            };
        }
    }
}
