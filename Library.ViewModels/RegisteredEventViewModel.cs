using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class RegisteredEventViewModel
    {
        public int EventId { get; set; }
        public string EventParticipantId { get; set; }
        public string EventTitle { get; set; }
        public string FullName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public DateTime RegisteredDate { get; set; }
        public ParticipantStatus ParticipantStatus { get; set; }

        public RegisteredEventViewModel()
        {
            
        }

        public RegisteredEventViewModel(EventParticipant participant)
        {
            EventId = participant.LibraryEventId;
            RegisteredDate = participant.RegisteredDate;
            ParticipantStatus = participant.ParticipantStatus;
            EventParticipantId = participant.ApplicationUserId;
        }

        public EventParticipant ConvertViewModelToModel(RegisteredEventViewModel model)
        {
            return new EventParticipant
            {
                LibraryEventId = model.EventId,
                RegisteredDate = model.RegisteredDate,
                ParticipantStatus = model.ParticipantStatus,
                ApplicationUserId = model.EventParticipantId
            };
        }
    }
}
