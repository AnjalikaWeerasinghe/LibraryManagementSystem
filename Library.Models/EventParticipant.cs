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
        public DateTime RegisteredDate { get; set; }
        public ParticipantStatus ParticipantStatus { get; set; }

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