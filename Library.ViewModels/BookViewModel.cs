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
        [StringLength(12)]
        public string ItemCode { get; set; } // ITD-BK-XXXXX
        [Required(ErrorMessage = "Enter a Title.")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter ISBN Number.")]
        [StringLength(50)]
        [RegularExpression(@"^(?i:ISBN)\s((?:\d[-]?){12}\d)$", ErrorMessage = "ISBN Number must start with 'ISBN', followed by 13 digits.")]
        public string ISBN { get; set; }
        [DataType("Published Year")]
        public int? PublishedYear { get; set; }
        [Required(ErrorMessage = "Select Edition.")]
        public string Edition { get; set; }
        [Required(ErrorMessage = "Select a language.")]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        [Required(ErrorMessage = "Select a category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Publisher")]
        [ForeignKey("Publisher")]
        public int? PublisherId { get; set; }
        [Display(Name = "Genre")]
        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        public string? ShelfLocation { get; set; }

        [Display(Name = "Number of Copies")]
        [Range(1, 100)]
        public int NumberOfCopies { get; set; }

        public int AvailableCopies { get; set; } 
        public int TotalCopies { get; set; }    
        public bool IsMemberView { get; set; }

        public Genre Genre { get; set; }
        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }

        public IEnumerable<LanguageViewModel> Languages { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<PublisherViewModel> Publishers { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; }

        public BookViewModel()
        {
        }

        public BookViewModel(Book model)
        {
            Id = model.Id;
            ItemCode = model.ItemCode;
            Title = model.Title;
            PublishedYear = model.PublishedYear;
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
                PublishedYear = model.PublishedYear,
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
