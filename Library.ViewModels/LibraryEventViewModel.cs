using Library.Models;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class LibraryEventViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^ED\d{4}$", ErrorMessage = "Event Code must be in the format ED0001.")]
        public string EventCode { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string CreatedBy { get; set; }

        public class EventSearchViewModel
        {
            public string SearchTerm { get; set; }
            public PagedResult<LibraryEventViewModel> Result { get; set; }
        }

        public LibraryEventViewModel()
        {
        }

        public LibraryEventViewModel(LibraryEvent model)
        {
            Id = model.Id;
            EventCode = model.EventCode;
            Title = model.Title;
            Description = model.Description;
            ImageUrl = model.ImageUrl;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Location = model.Location;
            CreatedBy = model.CreatedBy;
        }

        public LibraryEvent ConvertViewModel(LibraryEventViewModel model)
        {
            return new LibraryEvent
            {
                Id = model.Id,
                EventCode = model.EventCode,
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Location = model.Location,
                CreatedBy = model.CreatedBy
            };
        }
    }
}
