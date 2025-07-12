using Library.Utilities;
using Library.Utilities.Validation;
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
    public class ApplicationUser : IdentityUser, IUserRoleAccessor
    {
        [Required, StringLength(100)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string? CallingName { get; set; }

        [Required] [UserCodeFormat]
        public string UserCode { get; set; } = "";

        [Required]
        public Gender Gender { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; }

        public bool IsMember { get; set; }

        public string RoleName => IsMember ? WebSiteRoles.WebSite_Member : WebSiteRoles.WebSite_Staff;

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required]
        public UserStatus UserStatus { get; set; }

        [Url]
        public string? PictureUrl { get; set; }

        public string UserRole { get; set; } = WebSiteRoles.WebSite_Member;


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

