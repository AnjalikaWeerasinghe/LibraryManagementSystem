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
        // User Personnel Information
        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Calling Name")]
        public string? CallingName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Required, StringLength(250)]
        public string? Address { get; set; } = string.Empty;

        [Url]
        [Display(Name = "Profile Picture URL")]
        public string? PictureUrl { get; set; }


        public string Password { get; set; }

        [Required]
        [Display(Name = "User Code")]
        [UserCodeFormat]
        public string UserCode { get; set; } = string.Empty;

        [Required]
        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; } = UserStatus.Active;

        [Required, StringLength(20)]
        [Display(Name = "Role")]
        public string UserRole { get; set; } = WebSiteRoles.WebSite_Member;

        public bool IsMember => UserRole == WebSiteRoles.WebSite_Member;
        public bool IsStaff => UserRole == WebSiteRoles.WebSite_Staff;
        public bool IsLibrarian => UserRole == WebSiteRoles.WebSite_Librarian;
        public bool IsAdmin => UserRole == WebSiteRoles.WebSite_Admin;


        public ICollection<Payment> Payments { get; set; } = new List<Payment>();   
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

