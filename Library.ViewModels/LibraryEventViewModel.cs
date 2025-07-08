using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class LibraryEventViewModel
    {
        public int Id { get; set; }
        public string EventCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string CreatedBy { get; set; }

        public LibraryEventViewModel()
        {
        }

        public LibraryEventViewModel(LibraryEvent model)
        {
            Id = model.Id;
            EventCode = model.EventCode;
            Title = model.Title;
            Description = model.Description;
            Image = model.Image;
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
                Image = model.Image,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Location = model.Location,
                CreatedBy = model.CreatedBy
            };
        }
    }
}
