using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Periodical : LibraryItem
    {
        public string ISSN { get; set; }
        public Frequency Frequency { get; set; }
        public string Theme { get; set; }
    }
}

namespace Library.Models
{
    public enum Frequency
    {
        Daily, Weekly, Monthly, Quarterly
    }
}