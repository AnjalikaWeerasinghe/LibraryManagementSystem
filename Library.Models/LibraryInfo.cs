using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class LibraryInfo
    {
        public int Id { get; set; }
        public string RegisteredCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public string OpeningHours { get; set; }
        public string OpeningDays { get; set; }
        public string Description { get; set; }

        public ICollection<LibraryDivision> LibraryDivisions { get; set; }
        public ICollection<Vendor> Vendors { get; set; }
        public ICollection<Donor> Donors { get; set; }
        public ICollection<LibraryEvent> Events { get; set; }
    }
}
