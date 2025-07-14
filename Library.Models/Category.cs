using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType ItemType { get; set; }

        public ICollection<LibraryItem> LibraryItems { get; set; }
    }
}