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
    public class GenreViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public PagedResult<GenreViewModel> PagedGenres { get; set; }

        public GenreViewModel()
        {
        }

        public GenreViewModel(Genre model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }

        public Genre ConvertViewModel(GenreViewModel model)
        {
            return new Genre
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }
    }
}
