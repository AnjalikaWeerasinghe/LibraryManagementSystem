// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementSystem.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Output / TempData

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        #endregion

        #region InputModel

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required, Display(Name = "Full name")]
            public string FullName { get; set; }

            [Display(Name = "Calling name")]
            public string CallingName { get; set; }

            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "Gender")]
            public Gender? Gender { get; set; }

            [Display(Name = "Date of birth"), DataType(DataType.Date)]
            public DateTime? DOB { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
        #endregion

        #region Helpers

        private async Task LoadAsync(ApplicationUser user)
        {
            
            Username = await _userManager.GetUserNameAsync(user);

            Input = new InputModel
            {
                FullName = user.FullName,
                CallingName = user.CallingName,
                Address = user.Address,
                Gender = user.Gender,
                DOB = user.DOB,
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
            };
        }

        #endregion

        #region GET

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        #endregion

        #region POST

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            bool changed = false;

            if (user.FullName != Input.FullName) { user.FullName = Input.FullName; changed = true; }
            if (user.CallingName != Input.CallingName) { user.CallingName = Input.CallingName; changed = true; }
            if (user.Address != Input.Address) { user.Address = Input.Address; changed = true; }
            if (user.Gender != Input.Gender) { user.Gender = Input.Gender ?? Gender.Male; changed = true; }
            if (user.DOB != Input.DOB) { user.DOB = Input.DOB; changed = true; }

            if (changed)
                await _userManager.UpdateAsync(user); // persist custom fields

            // Refresh cookie
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
      
        }

        #endregion
        
    }
}
