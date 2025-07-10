using Library.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class BookViewModel
    {
        //Library Item Properties
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
        [DataType("Year")]
        public DateTime YearPublished { get; set; }
        [Required]
        public string ShelfLocation { get; set; }
        [Required]
        [Display(Name = "Language")]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        public List<Language> Languages { get; set; }
        [Required]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public List<SelectListItem> PublisherList { get; set; }
        [Required]
        [Display(Name = "Genre")]
        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        public List<SelectListItem> GenreList { get; set; }
        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // Properties of Book
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^(?i:ISBN)\s((?:\d[-]?){12}\d)$", ErrorMessage = "ISBN Number must start with 'ISBN', followed by 13 digits.")]
        public string ISBN { get; set; }
        [Required]
        public string Edition { get; set; }

        public Genre Genre { get; set; }
        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }

        public BookViewModel()
        {
        }

        public BookViewModel(Book model)
        {
            Id = model.Id;
            ItemCode = model.ItemCode;
            Title = model.Title;
            YearPublished = model.YearPublished;
            ShelfLocation = model.ShelfLocation;
            LanguageId = model.LanguageId;
            Language = model.Language;
            CategoryId = model.CategoryId;
            Category = model.Category;
            PublisherId = model.PublisherId;
            Publisher = model.Publisher;
            GenreId = model.GenreId;
            Genre = model.Genre;
            Description = model.Description;
            ISBN = model.ISBN;
            Edition = model.Edition;
        }

        public Book ConvertToViewModelToModel(BookViewModel model)
        {
            return new Book
            {
                Id = model.Id,
                ItemCode = model.ItemCode,
                Title = model.Title,
                YearPublished = model.YearPublished,
                ShelfLocation = model.ShelfLocation,
                LanguageId = model.LanguageId,
                Language = model.Language,
                CategoryId = model.CategoryId,
                Category = model.Category,
                PublisherId = model.CategoryId,
                Publisher = model.Publisher,
                Description = model.Description,
                ISBN = model.ISBN,
                Edition = model.Edition,
                GenreId = model.GenreId,
                Genre = model.Genre
            };
        }
    }
}
