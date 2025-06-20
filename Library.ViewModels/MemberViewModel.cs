using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MemberCode { get; set; }
        public DateTime MembershipDate { get; set; }
    }
}
