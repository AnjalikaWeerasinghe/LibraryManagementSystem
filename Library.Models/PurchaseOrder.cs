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
        public string PONumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CreatedBy { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int VendorId { get; set; }

        public Vendor Vendor { get; set; }

        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}

namespace Library.Models
{
    public enum OrderStatus
    {
        Pending, Cancelled, Delivered
    }
}