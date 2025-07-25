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
    public class CountryViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public PagedResult<CountryViewModel> PagedCountries { get; set; }

        public CountryViewModel()
        {
        }

        public CountryViewModel(Country model)
        {
            Id = model.Id;
            Name = model.Name;
        }

        public Country ConvertViewModel(CountryViewModel model)
        {
            return new Country
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public Country ToEntity()
        {
            return new Country
            {
                Id = this.Id,
                Name = this.Name,
            };
        }

        public static CountryViewModel FromEntity(Country country)
        {
            return new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}
