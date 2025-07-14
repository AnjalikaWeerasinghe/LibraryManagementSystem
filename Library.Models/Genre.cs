using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType? ItemType { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}