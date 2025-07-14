using Microsoft.AspNetCore.Mvc.Rendering;
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
        [StringLength(12)]
        public string ItemCode { get; set; }
        [Required(ErrorMessage = "Enter a Title.")]
        [StringLength(100)]
        public string Title { get; set; }
        public int? PublishedYear { get; set; }
        public ItemType ItemType { get; set; }
        [Required(ErrorMessage = "Select a language.")]
        public int LanguageId { get; set; }
        [Required(ErrorMessage = "Select a category.")]
        public int CategoryId { get; set; }
        public int? PublisherId { get; set; }
        public int? GenreId { get; set; } // Field can be optional
        public string? Description { get; set; }

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