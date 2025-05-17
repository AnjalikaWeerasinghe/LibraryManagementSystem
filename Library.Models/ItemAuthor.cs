namespace Library.Models
{
    public class ItemAuthor
    {
        public int Id { get; set; }
        public int LibraryItemId { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public LibraryItem LibraryItem { get; set; }
    }
}