using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class NotificationViewModel
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string? Link { get; set; }
    }
}
