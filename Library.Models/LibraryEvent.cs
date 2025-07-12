using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class LibraryEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^EID-\d{4}$")]
        public string EventCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(150)]
        public string Location { get; set; }

        public ICollection<EventParticipant> Participants { get; set; }
    }
}
