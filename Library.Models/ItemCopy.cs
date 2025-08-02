namespace Library.Models
{
    public class ItemCopy
    {
        public int Id { get; set; }
        public int LibraryItemId { get; set; }
        public string ItemCopyCode { get; set; }
        public string ShelfLocation { get; set; }
        public bool Available { get; set; } = true;

        public LibraryItem LibraryItem { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; }
    }

}

