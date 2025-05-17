using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int VendorId { get; set; }
        public int PaymentId { get; set; }

        public Vendor Vendor { get; set; }
        public Payment Payment { get; set; }

        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}

namespace Library.Models
{
    public enum OrderStatus
    {
        Pending, Cancelled, Completed
    }
}