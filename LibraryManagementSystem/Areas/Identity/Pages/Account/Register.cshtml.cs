// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Library.Models;
using Library.Services;
using Library.Utilities;
using Library.Utilities.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserCodeService _codeSvc;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserCodeService codeSvc)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _codeSvc = codeSvc;
        }


        [BindProperty]
        public InputModel Input { get; set; }

      
        public string ReturnUrl { get; set; }

       
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

    
        public class InputModel
        {
            //[Required]
            //[Display(Name = "UserName")]
            //[StringLength(20)]
            //public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Register as a Library member.")]
            public bool IsMember { get; set; }

            [UserCodeFormat]
            public string UserCode { get; set; } = string.Empty;

            public string? UserRole => IsMember ? "Member" : "Staff";
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
                return Page();

            string roleKey = Input.IsMember ? WebSiteRoles.WebSite_Member
                                            : WebSiteRoles.WebSite_Staff;

            string userCode = await _codeSvc.GenerateNextAsync(roleKey);

            Input.UserCode = userCode;
            TryValidateModel(Input);
            if (!ModelState.IsValid) return Page();

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                UserCode = userCode
            };

            IdentityResult result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError(string.Empty, e.Description);
                return Page();
            }

            await _userManager.AddToRoleAsync(user, Input.IsMember ? "Member" : "Staff");

            _logger.LogInformation("New user registered with code {Code}", userCode);


            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException(
                    $"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure it isn't abstract and has an empty constructor; " +
                    $"otherwise override the register page.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
