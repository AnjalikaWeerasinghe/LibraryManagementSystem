namespace Library.Models
{
    public class Member
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime MembershipDate { get; set; }
        public UserStatus UserStatus { get; set; }
        public int? MembershipTypeId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public MembershipType MembershipType { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Fine> Fines { get; set; }

    }
}

namespace Library.Models
{
    public enum UserStatus
    {
        Active, Inactive, Suspended, Expired
    }
}