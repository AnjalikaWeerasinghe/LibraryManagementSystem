using Library.Models;
using Library.Utilities;
using Library.Utilities.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class ApplicationUserViewModel : IUserRoleAccessor
    {
        public string? Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? UserName { get; set; }

        [Required]
        [Display(Name = "User Code"), ReadOnly(true)]
        [UserCodeFormat]
        public string UserCode { get; set; } = string.Empty; // LIB-MEM-00001 , LIB-STF-001


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
        public Gender Gender { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; } = string.Empty;

        [Url]
        [Display(Name = "Profile Picture URL")]
        public string? PictureUrl { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Role")]
        public string UserRole { get; set; } = WebSiteRoles.WebSite_Member;

        [Required]
        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; } = UserStatus.Active;


        public IEnumerable<SelectListItem>? GenderList { get; set; }
        public IEnumerable<SelectListItem>? UserStatusList { get; set; }
        public IEnumerable<SelectListItem>? RoleList { get; set; }

        public ApplicationUserViewModel()
        {
            
        }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            FullName = user.FullName;
            CallingName = user.CallingName;
            Gender = user.Gender;
            Address = user.Address;
            DOB = user.DOB;
            UserStatus = user.UserStatus;
            PictureUrl = user.PictureUrl;
            UserCode = user.UserCode;
        }

        public ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel user)
        {
            return new ApplicationUser
            {
                FullName = user.FullName,
                CallingName = user.CallingName,
                Gender = user.Gender,
                Address = user.Address,
                DOB = user.DOB,
                UserStatus = user.UserStatus,
                PictureUrl = user.PictureUrl,
                UserCode = user.UserCode
            };
        }

        public List<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
    }
}
