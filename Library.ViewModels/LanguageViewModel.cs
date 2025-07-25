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
    public class LanguageViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public PagedResult<LanguageViewModel> PagedLanguages { get; set; }

        public LanguageViewModel()
        {
        }

        public LanguageViewModel(Language model)
        {
            Id = model.Id;
            Name = model.Name;
        }

        public Language ConvertViewModel(LanguageViewModel model)
        {
            return new Language
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
