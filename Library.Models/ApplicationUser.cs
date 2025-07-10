using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? CallingName { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsMember { get; set; }
        public DateTime? DOB { get; set; }
        public UserStatus UserStatus { get; set; }
        public string? PictureUrl { get; set; }
        public string SelectedRole { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}

namespace Library.Models
{
    public enum Gender
    {
        Male, Female, Other
    }
}

namespace Library.Models
{
    public enum UserStatus
    {
        Active, Inactive, Suspended, Expired
    }
}
