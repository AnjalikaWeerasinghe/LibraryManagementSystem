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

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        public string? UserName { get; set; }

        [Required]
        [Display(Name = "User Code"), ReadOnly(true)]
        [UserCodeFormat]
        public string UserCode { get; set; } = string.Empty; // LIB-MEM-0001 , LIB-STF-0001

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

        [Required, StringLength(20)]
        [Display(Name = "Role")]
        public string UserRole { get; set; } 

        [Required]
        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; } 


        public IEnumerable<SelectListItem>? GenderList { get; set; }
        public IEnumerable<SelectListItem>? UserStatusList { get; set; }
        public List<SelectListItem>? RoleList { get; set; }

        public class UserSearchViewModel
        {
            public string SearchTerm { get; set; }
            public PagedResult<ApplicationUserViewModel> Result { get; set; }
        }

        public ApplicationUserViewModel()
        {
            
        }

        public ApplicationUserViewModel(ApplicationUser model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Id = model.Id;
            FullName = model.FullName;
            CallingName = model.CallingName;
            UserName = model.UserName;
            DOB = model.DOB;
            Email = model.Email;
            Gender = model.Gender;
            Address = model.Address;
            PictureUrl = model.PictureUrl;
            UserCode = model.UserCode;
            UserRole = model.UserRole;
            UserStatus = model.UserStatus;
        }

        public static ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel model)
        {
            return new ApplicationUser
            {

                FullName = model.FullName,
                CallingName = model.CallingName,
                UserName = model.UserName,
                DOB = model.DOB,
                Email = model.Email,
                Gender = model.Gender,
                Address = model.Address,
                PictureUrl = model.PictureUrl,
                UserCode = model.UserCode,
                UserRole = model.UserRole,
                UserStatus = model.UserStatus
            };
        }

        
        public List<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
    }
}
