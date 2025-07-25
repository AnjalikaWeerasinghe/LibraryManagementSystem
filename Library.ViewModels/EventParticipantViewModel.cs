using Library.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class EventParticipantViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Event")]
        public int LibraryEventId { get; set; }

        public string? EventTitle { get; set; } = string.Empty;

        public string? Location { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Registered Date")]
        public DateTime RegisteredDate { get; set; }

        [Display(Name = "Participant Status")]
        public ParticipantStatus ParticipantStatus { get; set; } = ParticipantStatus.Registered;

        [Display(Name = "User")]
        public string ApplicationUserId { get; set; } //Foreign key to Application User

        

        public EventParticipantViewModel()
        {
            
        }

        public EventParticipantViewModel(EventParticipant participant)
        {
            Id = participant.Id;
            LibraryEventId = participant.LibraryEventId;
            EventTitle = participant.LibraryEvent?.Title;
            Location = participant.LibraryEvent?.Location;
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
