using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class HomeContentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Main Title")]
        public string? Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Content { get; set; }
        public string? CustomLinks { get; set; }

        public List<string> EventImagePaths { get; set; } = new();


        public HomeContentViewModel()
        {
        }
    }
}
