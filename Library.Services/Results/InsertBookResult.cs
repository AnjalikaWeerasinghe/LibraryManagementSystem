using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Results
{
    public class InsertBookResult
    {
        public bool Success { get; set; }
        public int BookId { get; set; }
        public string Message { get; set; }
    }
}
