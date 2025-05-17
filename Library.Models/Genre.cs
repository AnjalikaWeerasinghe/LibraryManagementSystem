namespace Library.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<LibraryItem> LibraryItems { get; set; }
    }
}