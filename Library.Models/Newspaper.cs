using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Newspaper : LibraryItem
    {
        public string ISSN { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssueNumber { get; set; }
    }
}
