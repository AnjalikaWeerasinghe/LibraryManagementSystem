using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class ItemCategoryCountViewModel
    {
        public string ItemType { get; set; }   
        public string Category { get; set; }
        public int Count { get; set; }

        public Category Categories { get; set; }
    }
}
