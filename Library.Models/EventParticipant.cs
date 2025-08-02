using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class EventParticipant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LibraryEventId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public string? EventTitle { get; set; }

        public string? Location { get; set; }

        [Required]
        public ParticipantStatus ParticipantStatus { get; set; } = ParticipantStatus.Registered;

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public LibraryEvent LibraryEvent { get; set; }
    }
}

namespace Library.Models
{
    public enum ParticipantStatus
    {
        Registered, Cancelled, Attended
    }
}