namespace Library.Models
{
    public class Fine
    {
        public int Id { get; set; }
        public int BorrowingId { get; set; }
        public string FineCode { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public int FineTypeId { get; set; }

        public Borrowing Borrowing { get; set; }
        public FineType FineType { get; set; }
    }
}

namespace Library.Models
{
    public enum PaymentStatus
    {
        Overdue, Completed, Pending
    }
}