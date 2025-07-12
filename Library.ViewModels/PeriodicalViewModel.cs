using Library.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class PeriodicalViewModel 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        //[RegularExpression(@"^ITD\d{4}$", ErrorMessage = "Item Code must be in the format ITD0001.")]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public int PublishedYear { get; set; }
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
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string ISSN { get; set; }
        public Frequency Frequency { get; set; }
        public string Theme { get; set; }

        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }


        public PeriodicalViewModel()
        {
        }

        public PeriodicalViewModel(Periodical model)
        {
            Id = model.Id;
            ItemCode = model.ItemCode;
            Title = model.Title;
            PublishedYear = model.PublishedYear;
            ShelfLocation = model.ShelfLocation;
            LanguageId = model.LanguageId;
            Language = model.Language;
            CategoryId = model.CategoryId;
            Category = model.Category;
            PublisherId = model.PublisherId;
            Publisher = model.Publisher;
            Description = model.Description;

            ISSN = model.ISSN;
            Frequency = model.Frequency;
            Theme = model.Theme;
        }

        public Periodical ConvertToViewModelToModel(PeriodicalViewModel model)
        {
            return new Periodical
            {
                Id = model.Id,
                ItemCode = model.ItemCode,
                Title = model.Title,
                PublishedYear = model.PublishedYear,
                ShelfLocation = model.ShelfLocation,
                LanguageId = model.LanguageId,
                Language = model.Language,
                CategoryId = model.CategoryId,
                Category = model.Category,
                PublisherId = model.PublisherId,
                Publisher = model.Publisher,
                Description = model.Description,

                ISSN = model.ISSN,
                Frequency = model.Frequency,
                Theme = model.Theme

            };
        }
    }
}
