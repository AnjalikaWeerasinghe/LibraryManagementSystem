using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LibraryItem> LibraryItems { get; set; }
    }
}