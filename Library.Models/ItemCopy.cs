namespace Library.Models
{
    public class ItemCopy
    {
        public int Id { get; set; }
        public int LibraryItemId { get; set; }
        public string ItemCopyCode { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public string ShelfLocation { get; set; }

        public LibraryItem LibraryItem { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; }
    }

}

namespace Library.Models
{
    public enum ItemStatus
    {
        Available, Borrowed, Reserved, Lost, Damaged, ReferenceOnly
    }
}