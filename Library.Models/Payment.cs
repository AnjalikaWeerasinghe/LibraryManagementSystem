namespace Library.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int PurchaseOrderItemId { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public PurchaseOrderItem PurchaseOrderItem { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}