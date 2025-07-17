using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId)
                   ?? throw new KeyNotFoundException("User not found.");

            var current = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, current);
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));

            await _userManager.AddToRoleAsync(user, roleName);
            user.UserRole = roleName;
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CreateWithPasswordAsync(ApplicationUserViewModel user, string password)
        {
            var appuser = ToEntity(user);
            return await _userManager.CreateAsync(appuser, password);
        }

        public async Task<string> GenerateNextUserCodeAsync(bool isMember)
        {
            var prefix = isMember ? "LIB-MEM" : "LIB-STF";

            var last = await _userManager.Users
                         .Where(u => u.UserCode.StartsWith(prefix))
                         .OrderByDescending(u => u.UserCode)
                         .Select(u => u.UserCode)
                         .FirstOrDefaultAsync();

            var nextNum = (last != null && int.TryParse(last[^4..], out var n)) ? n + 1 : 1;
            return $"{prefix}-{nextNum:D4}";
        }

        public async Task<PagedResult<ApplicationUserViewModel>> GetAllAsync(int pageNumber, int pageSize)
          =>  await PagedUserAsync(_userManager.Users, pageNumber, pageSize); 
        

        private async Task<PagedResult<ApplicationUserViewModel>> 
            PagedUserAsync(IQueryable<ApplicationUser> users, int pageNumber, int pageSize)
        {
            var total = await users.CountAsync();
            var data = await users.OrderBy(u => u.UserCode)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = data.Select(ToViewModel).ToList(),
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<ApplicationUserViewModel>> GetAllMembersAsync(int pageNumber, int pageSize)
            => await PagedUserAsync(_userManager.Users
                .Where(u => u.UserRole == WebSiteRoles.WebSite_Member), pageNumber, pageSize);

        public async Task<PagedResult<ApplicationUserViewModel>> GetAllStaffAsync(int pageNumber, int pageSize)
            => await PagedUserAsync(_userManager.Users
                .Where(u => u.UserRole == WebSiteRoles.WebSite_Staff), pageNumber, pageSize);

        public async Task<ApplicationUserViewModel?> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user == null ? null : ToViewModel(user);
        }

        public async Task<PagedResult<ApplicationUserViewModel>> GetUserByUserCodeAsync(string usercode, int pageNumber, int pageSize)
            => await PagedUserAsync(
                _userManager.Users.Where(u => u.UserCode == usercode), pageNumber, pageSize);
 
        public async Task<PagedResult<ApplicationUserViewModel>> SearchMemberAsync(int pageNumber, int pageSize, string name)
            => await PagedUserAsync(
                _userManager.Users.Where(u => u.UserRole == WebSiteRoles.WebSite_Member &&
                     EF.Functions.Like(u.FullName, $"%{name}%")),pageNumber, pageSize);


        public async Task SetUserStatusAsync(string userId, UserStatus status)
        {
            var user = await _userManager.FindByIdAsync(userId)
                   ?? throw new KeyNotFoundException("User not found.");

            user.UserStatus = status;
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateApplicationUserAsync(ApplicationUserViewModel user)
        {
            var appuser = await _userManager.FindByIdAsync(user.Id!);
            if (appuser == null) throw new KeyNotFoundException("User not found.");

            ApplyViewModel(appuser, user);
            await _userManager.UpdateAsync(appuser);
        }


        private static ApplicationUserViewModel ToViewModel(ApplicationUser u) => new()
        {
            Id          = u.Id,
            Email       = u.Email,
            UserCode    = u.UserCode,
            FullName    = u.FullName,
            CallingName = u.CallingName,
            UserName    = u.Email,
            DOB         = u.DOB,
            Gender      = u.Gender,
            Address     = u.Address,
            PictureUrl  = u.PictureUrl,
            UserRole    = u.UserRole,
            UserStatus  = u.UserStatus,
            Password    = u.PasswordHash
        };

        private static ApplicationUser ToEntity(ApplicationUserViewModel vm) => new()
        {
            Id = vm.Id,
            Email = vm.Email,
            UserName = vm.Email,
            UserCode = vm.UserCode,
            FullName = vm.FullName,
            CallingName = vm.CallingName,
            DOB = vm.DOB,
            Gender = vm.Gender,
            Address = vm.Address,
            PictureUrl = vm.PictureUrl,
            UserRole = vm.UserRole,
            UserStatus = vm.UserStatus,
            PasswordHash = vm.Password
        };

        private static void ApplyViewModel(ApplicationUser u, ApplicationUserViewModel vm)
        {
            u.Email = vm.Email;
            u.UserName = vm.Email;
            u.FullName = vm.FullName;
            u.CallingName = vm.CallingName;
            u.DOB = vm.DOB;
            u.Gender = vm.Gender;
            u.Address = vm.Address;
            u.PictureUrl = vm.PictureUrl;
            u.UserStatus = vm.UserStatus;
            u.PasswordHash = vm.Password;
        }

        public async Task<IdentityResult> InsertApplicationUserAsync(ApplicationUserViewModel user, string? password = null)
        {
            var appuser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.UserName ?? user.Email,    // or UserCode
                FullName = user.FullName,
                CallingName = user.CallingName,
                Gender = user.Gender,
                Address = user.Address,
                DOB = user.DOB,
                PictureUrl = user.PictureUrl,
                UserStatus = user.UserStatus,
                UserRole = user.UserRole,
                PasswordHash = user.Password,
                UserCode = user.UserCode ?? await GenerateNextUserCodeAsync(user.UserRole == WebSiteRoles.WebSite_Member)
            };

            //IdentityResult result = password is null
            //    ? await _userManager.CreateAsync(appuser)              // no password yet
            //    : await _userManager.CreateAsync(appuser, password);   // sets password

            //if (!result.Succeeded)
            //    return result;   

            
            //if (await _roleManager.RoleExistsAsync(user.UserRole))
            //    await _roleManager.CreateAsync(new IdentityRole(user.UserRole));

            await _userManager.AddToRoleAsync(appuser, user.UserRole);

            return IdentityResult.Success;
        }


    }
}
