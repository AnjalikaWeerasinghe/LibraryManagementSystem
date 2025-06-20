namespace Library.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public int MemeberId { get; set; }
        public int ItemCopyId { get; set; }
        public string BorrowedCode { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public BorrowedStatus BorrowedStatus { get; set; }

        public Member Member { get; set; }
        public ItemCopy ItemCopy { get; set; }

        public ICollection<Fine> Fines { get; set; }
    }
}

namespace Library.Models
{
    public enum BorrowedStatus
    {
        Returned, Pending
    }
}