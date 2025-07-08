using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Landline { get; set; }

        public ICollection<LibraryItem> LibraryItems { get; set; }
    }
}