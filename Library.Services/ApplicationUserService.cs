using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
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

        public PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<ApplicationUser>()
                    .GetAll()
                    .OrderBy(u => u.UserCode)
                    .Skip(ExcludeRecords)
                    .Take(pageSize)
                    .ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>()
                    .GetAll()
                    .ToList()
                    .Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public ApplicationUserViewModel GetUserById(string userId)
        {
            var model = _unitOfWork.GenericRepository<ApplicationUser>().GetById(userId);
            var vm = new ApplicationUserViewModel(model);
            return vm;
        }

        public async Task<IdentityResult> InsertApplicationUserAsync(ApplicationUserViewModel appuser, string password)
        {
            var model = ApplicationUserViewModel.ConvertViewModelToModel(appuser);

            model.UserName = model.Email;

            var result = await  _userManager.CreateAsync(model,password);

            if (result.Succeeded)
            {
               await _userManager.AddToRoleAsync(model, appuser.UserRole);
            }

            return result;
        }

        public PagedResult<ApplicationUserViewModel> SearchUserByFullName(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<ApplicationUser>()
                .GetAll()
                .Where(p => p.FullName.Contains(name))
                .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new ApplicationUserViewModel
            {
                UserCode = p.UserCode,
                FullName = p.FullName,
                CallingName = p.CallingName,
                Gender = p.Gender,
                Address = p.Address,
                DOB = p.DOB,
                UserStatus = p.UserStatus,
                PictureUrl = p.PictureUrl,
                UserRole = p.UserRole,
                Email = p.Email,
                UserName = p.UserName
            }).ToList();

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IdentityResult> UpdateApplicationUserAsync(ApplicationUserViewModel appuser)
        {
            var user = await _userManager.FindByIdAsync(appuser.Id);
            if (user == null)
                throw new Exception("User not found");

            var existingEmailUser = await _userManager.FindByEmailAsync(appuser.Email);
            if (existingEmailUser != null && existingEmailUser.Id != appuser.Id)
                throw new Exception("Email is already in use.");

            user.FullName = appuser.FullName;
            user.CallingName = appuser.CallingName;
            user.Gender = appuser.Gender;
            user.Address = appuser.Address;
            user.DOB = appuser.DOB;
            user.UserStatus = appuser.UserStatus;
            user.PictureUrl = appuser.PictureUrl;
            user.UserName = appuser.Email;
            user.Email = appuser.Email;

            if (Enum.IsDefined(typeof(UserStatus), appuser.UserStatus))
            {
                user.UserStatus = appuser.UserStatus;
            }
            else
            {
                user.UserStatus = UserStatus.Active; // default fallback
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any(r => r != appuser.UserRole))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                var roleExists = await _roleManager.RoleExistsAsync(appuser.UserRole);
                if (!roleExists)
                    throw new Exception("Assigned role does not exist.");

                await _userManager.AddToRoleAsync(user, appuser.UserRole);
            }

            return await _userManager.UpdateAsync(user);
        }

        private List<ApplicationUserViewModel> ConvertModelToViewModelList(List<ApplicationUser> modelList)
        {
            return modelList.Select(x => new ApplicationUserViewModel(x)).ToList();
        }

        public List<SelectListItem> BuildRoleList()
        {
            return new List<SelectListItem>
                {
                    new SelectListItem { Value = WebSiteRoles.WebSite_Admin, Text = "Admin" },
                    new SelectListItem { Value = WebSiteRoles.WebSite_Staff, Text = "Staff" },
                    new SelectListItem { Value = WebSiteRoles.WebSite_Member, Text = "Member" }
                };
        }

        public async Task SetUserStatusAsync(string userId, UserStatus status)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.UserStatus = status;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to update user status");
        }

        public async Task ToggleUserStatusAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.UserStatus = user.UserStatus == UserStatus.Active
                ? UserStatus.Inactive
                : UserStatus.Active;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to update user status");
        }

        public async Task<IEnumerable<SelectListItem>> GetAllMembersAsSelectListAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");

            return users.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = $"{u.FullName} {u.UserCode} "
            }).ToList();
        }
    }
}
