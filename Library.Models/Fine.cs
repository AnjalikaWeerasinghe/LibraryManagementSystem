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
        [StringLength(10)]
        [RegularExpression(@"^FD_\d{4}$", ErrorMessage = "Fine Code must be in the format FD_0001.")]
        public string FineCode { get; set; }
        [Required]
        [Range(20.00, 1000.00, ErrorMessage = "Amount must be between Rs.20.00 and Rs.1000.00.")]
        public decimal Amount { get; set; }
        [Required]
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime IssuedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; }
        [Required]
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