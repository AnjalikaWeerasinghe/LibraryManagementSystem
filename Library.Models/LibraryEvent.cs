namespace Library.Models
{
    public class LibraryEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string CreatedBy { get; set; }

        public int LibraryInfoId { get; set; }
        public LibraryInfo LibraryInfo { get; set; }

        public ICollection<EventParticipant> Participants { get; set; }
    }
}
