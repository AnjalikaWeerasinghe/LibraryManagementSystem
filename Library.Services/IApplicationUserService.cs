using Library.Models;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IApplicationUserService
    {
        PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize);

        //Task<PagedResult<ApplicationUserViewModel>> GetAllMembersAsync(int pageNumber, int pageSize);
        //Task<PagedResult<ApplicationUserViewModel>> GetAllStaffAsync(int pageNumber, int pageSize);

        PagedResult<ApplicationUserViewModel> SearchUserByFullName(string name, int pageNumber, int pageSize);

        //Task<PagedResult<ApplicationUserViewModel>> GetUserByUserCodeAsync(string usercode, int pageNumber, int pageSize);
        //Task<IdentityResult> CreateWithPasswordAsync(ApplicationUserViewModel user, string password);

        Task<IdentityResult> UpdateApplicationUserAsync(ApplicationUserViewModel appuser);
        Task<IdentityResult> InsertApplicationUserAsync(ApplicationUserViewModel appuser, string password);

        ApplicationUserViewModel GetUserById(string userId);
        Task<string> GenerateNextUserCodeAsync(bool isMember);

        //Task AssignRoleAsync(string userId, string roleName);
        Task SetUserStatusAsync(string userId, UserStatus status);

        Task ToggleUserStatusAsync(string userId);

        List<SelectListItem> BuildRoleList();

        Task<IEnumerable<SelectListItem>> GetAllMembersAsSelectListAsync();
    }
}
