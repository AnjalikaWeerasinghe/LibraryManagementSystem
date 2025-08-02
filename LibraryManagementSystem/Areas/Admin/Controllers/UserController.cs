using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly IApplicationUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IApplicationUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<ApplicationUserViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _userService.SearchUserByFullName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _userService.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ApplicationUserViewModel
            {
                GenderList = Enum.GetValues(typeof(Gender))
                    .Cast<Gender>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    }),

                UserStatusList = Enum.GetValues(typeof(UserStatus))
                    .Cast<UserStatus>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    }),

                RoleList = typeof(WebSiteRoles)
                    .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .Where(f => f.IsLiteral && !f.IsInitOnly)
                    .Select(f => new SelectListItem
                    {
                        Value = f.GetValue(null)?.ToString(),
                        Text = f.GetValue(null)?.ToString()
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserViewModel vm)
        {
            bool isMember = vm.UserRole == WebSiteRoles.WebSite_Member;
            vm.UserCode =await _userService.GenerateNextUserCodeAsync(isMember);

            await _userService.InsertApplicationUserAsync(vm, vm.Password);
            TempData["SuccessMessage"] = $"{vm.FullName} successfully added!";

            return RedirectToAction(nameof(Create));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _userService.GetUserById(id);

            user.GenderList = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(f => new SelectListItem
                {
                    Value = f.ToString(),
                    Text = f.ToString(),
                    Selected = (user.Gender == f)
                });

            user.UserStatusList = Enum.GetValues(typeof(UserStatus))
                .Cast<UserStatus>()
                .Select(f => new SelectListItem
                {
                    Value = f.ToString(),
                    Text = f.ToString(),
                    Selected = (user.UserStatus == f)
                });

            user.RoleList = typeof(WebSiteRoles)
                .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .Where(f => f.IsLiteral && !f.IsInitOnly)
                .Select(f => new SelectListItem
                {
                    Value = f.GetValue(null)?.ToString(),
                    Text = f.GetValue(null)?.ToString(),
                    Selected = (user.UserRole == f.GetValue(null)?.ToString())
                }).ToList();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserViewModel model)
        {

            var user = _unitOfWork.GenericRepository<ApplicationUser>().GetById(model.Id);
            if (user == null)
                return NotFound();

            user.Gender = model.Gender;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Address = model.Address;
            user.CallingName = model.CallingName;
            user.DOB = model.DOB;
            user.UserStatus = model.UserStatus;
            user.PictureUrl = model.PictureUrl;
            user.FullName = model.FullName;

            await _userService.UpdateApplicationUserAsync(model);

            TempData["SuccessMessage"] = $"{model.FullName} updated successfully!";
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            try
            {
                await _userService.ToggleUserStatusAsync(id);
                TempData["SuccessMessage"] = "User status updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}
