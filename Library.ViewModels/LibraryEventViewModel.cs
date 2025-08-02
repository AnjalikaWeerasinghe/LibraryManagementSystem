using Library.Models;
using Library.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [StringLength(8)]
        [RegularExpression(@"^EID-\d{4}$", ErrorMessage = "Event Code must be in the format EID-0001.")]
        public string EventCode { get; set; }  // EID-0001

        [Required(ErrorMessage = "Include title for event.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Add a description.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Url]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Include a event starting date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Add the location of the event.")]
        [StringLength(150, ErrorMessage = "Location cannot exceed 150 characters.")]
        public string Location { get; set; }

        public EventStatus EventStatus { get; set; }

        public IEnumerable<SelectListItem>? EventStatusList { get; set; }

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
            EventStatus = model.EventStatus;
           
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
                EventStatus = model.EventStatus
            };
        }
    }
}
