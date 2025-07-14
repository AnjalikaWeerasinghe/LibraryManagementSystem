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
        [StringLength(12)]
        public string ItemCode { get; set; } // ITD-PD-XXXXX
        [Required(ErrorMessage = "Enter a Title.")]
        [StringLength(100)]
        public string Title { get; set; }
        [Range(1900, 2100, ErrorMessage = "Enter a year between 1900 and Present.")]
        public int? PublishedYear { get; set; }
        [Required(ErrorMessage = "Select a language.")]
        [Display(Name = "Language")]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        [Required(ErrorMessage = "Select a Category.")]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Publisher")]
        [ForeignKey("Publisher")]
        public int? PublisherId { get; set; }
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Enter a ISSN Number.")]
        [StringLength(9)]
        [RegularExpression(@"^\d{4}-\d{3}[\dXx]$", ErrorMessage = "Invalid ISSN format. Use XXXX-XXXX format.")]
        public string ISSN { get; set; }
        [Required(ErrorMessage = "Select a frequency.")]
        public Frequency Frequency { get; set; }
        [Required(ErrorMessage = "Enter a theme.")]
        public string Theme { get; set; }

        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }

        public IEnumerable<LanguageViewModel> Languages { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<PublisherViewModel> Publishers { get; set; }
        public IEnumerable<SelectListItem> FrequencyList { get; set; }

        public PeriodicalViewModel()
        {
        }

        public PeriodicalViewModel(Periodical model)
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
