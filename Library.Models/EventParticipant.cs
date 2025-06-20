using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class EventParticipant
    {
        public int Id { get; set; }
        public int LibraryEventId { get; set; }
        public string UserId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ParticipantStatus ParticipantStatus { get; set; }

        public LibraryEvent LibraryEvent { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

namespace Library.Models
{
    public enum ParticipantStatus
    {
        Registered, Cancelled, Attended
    }
}