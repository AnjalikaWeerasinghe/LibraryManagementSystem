using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public abstract class LibraryItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^ITD\d{4}$", ErrorMessage = "Item Code must be in the format ITD0001.")]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime YearPublished { get; set; }
        public ItemType ItemType { get; set; }
        [Required]
        public string ShelfLocation { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int PublisherId { get; set; }
        public int? GenreId { get; set; } // Field can be optional
        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name ="Description")]
        public string Description { get; set; }

        public Genre Genre { get; set; }
        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<ItemCopy> Copies { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ItemAuthor> ItemAuthors { get; set; }
    }

}

namespace Library.Models
{
    public enum ItemType
    {
        Book, Newspaper, Journal, Periodical
    }
}