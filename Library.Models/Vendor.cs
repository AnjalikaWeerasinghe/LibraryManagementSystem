namespace Library.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string VendorId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        public int LibraryInfoId { get; set; }
        public LibraryInfo LibraryInfo { get; set; }

        public ICollection<Acquisition> Acquisitions { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}