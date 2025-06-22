using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public abstract class LibraryItem
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Title { get; set; }
        public DateTime YearPublished { get; set; }
        public ItemType ItemType { get; set; }
        public string ShelfLocation { get; set; }
        public int LanguageId { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public string Description { get; set; }

        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<ItemCopy> Copies { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }

}

namespace Library.Models
{
    public enum ItemType
    {
        Book, Newspaper, Journal, Periodical
    }
}