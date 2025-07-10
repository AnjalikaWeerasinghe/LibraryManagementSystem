using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class ApplicationUserViewModel
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

        public ApplicationUserViewModel()
        {
            
        }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            FullName = user.FullName;
            CallingName = user.CallingName;
            Gender = user.Gender;
            Email = user.Email;
            Address = user.Address;
            IsMember = user.IsMember;
            DOB = user.DOB;
            UserStatus = user.UserStatus;
            PictureUrl = user.PictureUrl;
            UserName = user.UserName;
            SelectedRole = user.SelectedRole;
            UserCode = user.UserCode;
        }

        public ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel user)
        {
            return new ApplicationUser
            {
                FullName = user.FullName,
                CallingName = user.CallingName,
                Gender = user.Gender,
                Email = user.Email,
                Address = user.Address,
                IsMember = user.IsMember,
                DOB = user.DOB,
                UserStatus = user.UserStatus,
                PictureUrl = user.PictureUrl,
                UserName = user.UserName,
                SelectedRole = user.SelectedRole,
                UserCode = user.UserCode
            };
        }

        public List<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
    }
}
