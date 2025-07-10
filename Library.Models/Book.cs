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
        public string ISBN { get; set; }
        public string Edition { get; set; }

    }
}
