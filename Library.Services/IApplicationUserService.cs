using Library.Models;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IApplicationUserService
    {
        Task<PagedResult<ApplicationUserViewModel>> GetAllAsync(int pageNumber, int pageSize);

        Task<PagedResult<ApplicationUserViewModel>> GetAllMembersAsync(int pageNumber, int pageSize);
        Task<PagedResult<ApplicationUserViewModel>> GetAllStaffAsync(int pageNumber, int pageSize);

        Task<PagedResult<ApplicationUserViewModel>> SearchMemberAsync(int pageNumber, int pageSize, string name);
        Task<PagedResult<ApplicationUserViewModel>> GetUserByUserCodeAsync(string usercode, int pageNumber, int pageSize);
        Task<IdentityResult> CreateWithPasswordAsync(ApplicationUserViewModel user, string password);

        Task UpdateApplicationUserAsync(ApplicationUserViewModel user);
        Task<IdentityResult> InsertApplicationUserAsync(ApplicationUserViewModel user, string? password = null);

        Task<ApplicationUserViewModel?> GetByIdAsync(string userId);
        Task<string> GenerateNextUserCodeAsync(bool isMember);

        Task AssignRoleAsync(string userId, string roleName);
        Task SetUserStatusAsync(string userId, UserStatus status);
    }
}
