using Library.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public ItemType ItemType { get; set; }

        public IEnumerable<SelectListItem> ItemTypeList { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(Category model)
        {
            Id = model.Id;
            Name = model.Name;
            ItemType = model.ItemType;
        }

        public Category ConvertViewModel(CategoryViewModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                ItemType = model.ItemType
            };
        }
    }
}
