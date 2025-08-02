using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class LibraryEvent
    {
        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^EID-\d{4}$")]
        [StringLength(8)]
        public string EventCode { get; set; } // EID-0001

        [Required(ErrorMessage = "Include title for event.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Add a description.")]
        public string Description { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Include a event starting date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Add the location of the event.")]
        public string Location { get; set; }

        public EventStatus EventStatus { get; set; } = EventStatus.Ongoing;

        public ICollection<EventParticipant> Participants { get; set; }
    }
}

namespace Library.Models
{
    public enum EventStatus
    {
        Cancel, Ongoing
    }
}