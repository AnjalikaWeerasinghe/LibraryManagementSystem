using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Journal : LibraryItem
    {
        public string ISSN { get; set; }
        public string Volume { get; set; }
        public string Issue { get; set; }
        public int FieldOfStudyId { get; set; }

        public FieldOfStudy FieldOfStudy { get; set; }
    }
}
