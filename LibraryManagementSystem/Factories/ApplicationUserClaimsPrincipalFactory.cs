using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Models; 

namespace LibraryManagementSystem.Factories
{
    public class ApplicationUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // FullName - handle null
            identity.AddClaim(new Claim("FullName", user.FullName ?? ""));

            // UserStatus - safely handle missing or invalid enums
            if (Enum.IsDefined(typeof(UserStatus), user.UserStatus))
            {
                Console.WriteLine($"[Warning] Invalid UserStatus for user: {user.Id}");
                identity.AddClaim(new Claim("UserStatus", user.UserStatus.ToString()));
            }
            else
            {
                identity.AddClaim(new Claim("UserStatus", UserStatus.Active.ToString())); // or skip adding the claim
            }

            // UserRole - handle null
            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRole ?? ""));

            return identity;
        }
    }
}

