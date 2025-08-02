using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class ItemCopyViewModel
    {
        public int Id { get; set; }
        public int LibraryItemId { get; set; }
        public string ItemCopyCode { get; set; }
        public string ShelfLocation { get; set; }
        public bool Available { get; set; }

        public ItemCopyViewModel()
        {
            
        }


    }
}
