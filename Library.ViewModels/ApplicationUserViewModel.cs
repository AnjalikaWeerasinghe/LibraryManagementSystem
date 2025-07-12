using Library.Models;
using Library.Utilities;
using Library.Utilities.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class ApplicationUserViewModel : IUserRoleAccessor
    {
        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(50)]
        [Display(Name = "Calling Name")]
        public string? CallingName { get; set; }

        [Required]
        [Display(Name = "User Code")]
        [UserCodeFormat]
        public string UserCode { get; set; } = "";

        [Required]
        public Gender Gender { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; }

        public bool IsMember { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }

        [Required]
        public UserStatus UserStatus { get; set; }

        [Url]
        [Display(Name = "Photo URL")]
        public string? PictureUrl { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string SelectedRole {  get; set; } = WebSiteRoles.WebSite_Member;

        string IUserRoleAccessor.UserRole => SelectedRole;

        public IEnumerable<SelectListItem>? GenderList { get; set; }
        public IEnumerable<SelectListItem>? UserStatusList { get; set; }



        public ApplicationUserViewModel()
        {
            
        }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            FullName = user.FullName;
            CallingName = user.CallingName;
            Gender = user.Gender;
            Address = user.Address;
            IsMember = user.IsMember;
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
                IsMember = user.IsMember,
                DOB = user.DOB,
                UserStatus = user.UserStatus,
                PictureUrl = user.PictureUrl,
                UserCode = user.UserCode
            };
        }

        public List<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
    }
}
