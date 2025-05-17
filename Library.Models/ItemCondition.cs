namespace Library.Models
{
    public class ItemCondition
    {
        public int Id { get; set; }
        public string Condition { get; set; }

        public ICollection<AcquisitionItem> AcquisitionItems { get; set; }
    }
}