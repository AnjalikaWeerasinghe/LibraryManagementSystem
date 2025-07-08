using Library.Models;
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
    }
}
