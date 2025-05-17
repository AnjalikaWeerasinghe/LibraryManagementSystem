namespace Library.Models
{
    public class AcquisitionItem
    {
        public int Id { get; set; }
        public int AcquisitionId { get; set; }
        public int LibraryItemId { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public int ItemConditionId { get; set; }

        public Acquisition Acquisition { get; set; }
        public LibraryItem LibraryItem { get; set; }
        public ItemCondition ItemCondition { get; set; }
    }
}