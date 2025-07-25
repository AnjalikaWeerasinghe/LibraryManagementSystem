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
    public class AuthorViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        
        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Biography")]
        public string Biography { get; set; }

        public Country Country { get; set; }

        //public IEnumerable<CountryViewModel> Countries { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public PagedResult<AuthorViewModel> PagedAuthors { get; set; }


        public class AuthorSearchViewModel
        {
            public string SearchTerm { get; set; }
            public PagedResult<AuthorViewModel> Result { get; set; }
        }

        public AuthorViewModel()
        {
        }

        public AuthorViewModel(Author model)
        {
            Id = model.Id;
            Name = model.Name;
            CountryId = model.CountryId;
            Biography = model.Biography;
            Country = model.Country;
        }

        public Author ConvertViewModel(AuthorViewModel model)
        {
            return new Author
            {
                Id = model.Id,
                Name = model.Name,
                CountryId = model.CountryId,
                Biography = model.Biography,
                Country = model.Country
            };
        }
    }
}
