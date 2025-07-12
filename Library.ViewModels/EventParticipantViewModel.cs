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

        [Required(ErrorMessage = "Please select an event.")]
        [Display(Name = "Event")]
        public int LibraryEventId { get; set; }

        [Required(ErrorMessage = "Please enter a registration date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Registered Date")]
        public DateTime RegisteredDate { get; set; }

        [Required(ErrorMessage = "Please select a participation status.")]
        [Display(Name = "Participant Status")]
        public ParticipantStatus ParticipantStatus { get; set; } = ParticipantStatus.Registered;

        [Required(ErrorMessage = "User is required.")]
        [Display(Name = "User")]
        public string ApplicationUserId { get; set; } //Foreign key to Application User

        public List<SelectListItem> Events { get; set; }
        public List<SelectListItem> Users { get; set; }

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
