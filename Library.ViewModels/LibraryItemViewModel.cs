using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.ViewModels
{
    public abstract class LibraryItemViewModel
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

        public Genre Genre { get; set; }
        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }

        // Book
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^(?i:ISBN)\s((?:\d[-]?){12}\d)$", ErrorMessage = "ISBN Number must start with 'ISBN', followed by 13 digits.")]
        public string? ISBN { get; set; }
        [Required]
        public string? Edition { get; set; }


        protected LibraryItemViewModel()
        {
        }

        public abstract LibraryItem ToDomainModel();

        public LibraryItemViewModel ConvertViewModel(LibraryItem model)
        {
            return model switch
            {
                
                
                Journal journal => new JournalViewModel
                {
                    Id = journal.Id,
                    ItemCode = journal.ItemCode,
                    Title = journal.Title,
                    YearPublished = journal.YearPublished,
                    ShelfLocation = journal.ShelfLocation,
                    LanguageId = journal.LanguageId,
                    CategoryId = journal.CategoryId,
                    PublisherId = journal.CategoryId,
                    Description = journal.Description,
                    ISSN = journal.ISSN,
                    Volume = journal.Volume,
                    Issue = journal.Issue,
                    Field = journal.Field
                },
                Periodical periodical => new PeriodicalViewModel
                {
                    Id = periodical.Id,
                    ItemCode = periodical.ItemCode,
                    Title = periodical.Title,
                    YearPublished = periodical.YearPublished,
                    ShelfLocation = periodical.ShelfLocation,
                    LanguageId = periodical.LanguageId,
                    CategoryId = periodical.CategoryId,
                    PublisherId = periodical.CategoryId,
                    Description = periodical.Description,
                    ISSN = periodical.ISSN,
                    Frequency = periodical.Frequency.ToString(),
                    Theme = periodical.Theme
                }

            };
        }

        protected LibraryItemViewModel(LibraryItem model)
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
        }
        
    }
}

namespace Library.Models
{
    public enum ItemType
    {
        Book, Newspaper, Journal, Periodical
    }
}
