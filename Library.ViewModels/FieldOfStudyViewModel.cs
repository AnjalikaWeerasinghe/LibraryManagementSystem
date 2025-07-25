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
    public class FieldOfStudyViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public PagedResult<FieldOfStudyViewModel> PagedFields { get; set; }

        public FieldOfStudyViewModel()
        {
            
        }

        public FieldOfStudyViewModel(FieldOfStudy fieldOfStudy)
        {
            Id = fieldOfStudy.Id;
            Name = fieldOfStudy.Name;
        }

        public FieldOfStudy ToModel(FieldOfStudyViewModel model)
        {
            return new FieldOfStudy
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
