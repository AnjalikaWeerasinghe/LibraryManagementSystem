namespace Library.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int LibraryItemId { get; set; }
        public string ReservationCode { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public ReservedStatus ReservedStatus { get; set; }

        public LibraryItem LibraryItem { get; set; }
        public Member Member { get; set; }
    }
}

namespace Library.Models
{
    public enum ReservedStatus
    {
        Fulfilled, Pending, Cancelled, Approved
    }
}