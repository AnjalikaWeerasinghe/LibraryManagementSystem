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
    public class JournalViewModel 
    {
        [Key]
        public int Id { get; set; }
        [StringLength(12)]
         public string ItemCode { get; set; } // ITD-JR-XXXXX
        [Required(ErrorMessage = "Enter a Title.")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter a ISSN Number.")]
        [StringLength(9)]
        [RegularExpression(@"^\d{4}-\d{3}[\dXx]$", ErrorMessage = "Invalid ISSN format. Use XXXX-XXXX format.")]
        public string ISSN { get; set; }
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Range(1900, 2100, ErrorMessage = "Enter a year between 1900 and Present.")]
        [Display(Name = "Published Year")]
        public int? PublishedYear { get; set; }
        [Required(ErrorMessage = "Select a language.")]
        [Display(Name = "Language")]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        [Required(ErrorMessage = "Select a category.")]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Publisher")]
        [ForeignKey("Publisher")]
        public int? PublisherId { get; set; }
        [Required(ErrorMessage = "Select a field.")]
        [Display(Name = "Field")]
        [ForeignKey("FieldOfStudy")]
        public int FieldOfStudyId { get; set; }
        [Required(ErrorMessage = "Select a Volume.")]
        public string Volume { get; set; }
        [Required(ErrorMessage ="Select an Issue.")]
        public string Issue { get; set; }
        

        public Language Language { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public FieldOfStudy FieldOfStudy { get; set; }

        public IEnumerable<LanguageViewModel> Languages { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<PublisherViewModel> Publishers { get; set; }
        public IEnumerable<FieldOfStudyViewModel> Fields { get; set; }

        public JournalViewModel()
        {
        }

        public JournalViewModel(Journal model)
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
            Volume = model.Volume;
            Issue = model.Issue;
            FieldOfStudyId = model.FieldOfStudyId;
            FieldOfStudy = model.FieldOfStudy;
        }

        public Journal ConvertToViewModelToModel(JournalViewModel model)
        {
            return new Journal
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
                Volume = model.Volume,
                Issue = model.Issue,
                FieldOfStudyId = model.FieldOfStudyId,
                FieldOfStudy = model.FieldOfStudy
            };
        }
    }
}
