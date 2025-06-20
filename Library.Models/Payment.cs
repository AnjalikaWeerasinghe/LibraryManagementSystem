namespace Library.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int? PurchaseOrderId { get; set; }
        public string UserId { get; set; }
        public string PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
        public ApplicationUser Librarian { get; set; }
    }
}

namespace Library.Models
{
    public enum PaymentMethod
    {
        Cash, credit_Debit_Card
    }
}