using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Utilities.Validation
{
    public class UserCodeFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext ctx)
        {
            if (ctx.ObjectInstance is not IUserRoleAccessor accessor)
                return ValidationResult.Success;   // nothing to validate

            string role = accessor.UserRole ?? "";
            string code = value as string ?? "";

            string pattern = role switch
            {
                WebSiteRoles.WebSite_Member => @"^LIB\-MEM\-\d{4}$",
                WebSiteRoles.WebSite_Staff => @"^LIB\-STF\-\d{4}$",
                _ => null
            };

            bool ok = pattern is not null && Regex.IsMatch(code, pattern);

            return ok
                ? ValidationResult.Success
                : new ValidationResult(
                    $"UserCode must match {(role == WebSiteRoles.WebSite_Member ? "LIB‑MEM‑0001" : "LIB‑STF‑0001")} format.");

        }
    }
}
