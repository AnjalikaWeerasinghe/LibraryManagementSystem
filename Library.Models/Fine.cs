using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Fine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BorrowingId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime IssuedDate { get; set; }
        public bool IsPaid { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; }

        public Borrowing Borrowing { get; set; }
    }
}

namespace Library.Models
{
    public enum PaymentStatus
    {
        Overdue, Completed, Pending
    }
}