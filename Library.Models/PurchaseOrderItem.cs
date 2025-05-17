namespace Library.Models
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int LibraryItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public LibraryItem LibraryItem { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}