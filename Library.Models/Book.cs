using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book : LibraryItem
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^(?i:ISBN)\s((?:\d[-]?){12}\d)$", ErrorMessage = "ISBN Number must start with 'ISBN', followed by 13 digits.")]
        public string ISBN { get; set; }
        [Required]
        public string Edition { get; set; }

    }
}
