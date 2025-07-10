using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class AboutViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Content")]
        public string Content { get; set; }
        public string? ImageUrl { get; set; }


        public AboutViewModel()
        {
        }
    }
}
