using Library.Models;
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
        private const int PageSize = 20;

        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        // GET: /Admin/ApplicationUser?page=1
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = await _userService.GetAllAsync(page, PageSize);
            return View(model);
        }

        // GET: /Admin/ApplicationUser/Members
        public async Task<IActionResult> Members(int page = 1)
        {
            var model = await _userService.GetAllMembersAsync(page, PageSize);
            return View("Index", model);
        }

        // GET: /Admin/ApplicationUser/Staff
        public async Task<IActionResult> Staff(int page = 1)
        {
            var model = await _userService.GetAllStaffAsync(page, PageSize);
            return View("Index", model);
        }

        // GET: /Admin/ApplicationUser/Edit/123
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var vm = await _userService.GetByIdAsync(id);
            if (vm == null) return NotFound();

            vm.RoleList = new List<SelectListItem>
            {
                new("Admin",      WebSiteRoles.WebSite_Admin),
                new("Librarian",  WebSiteRoles.WebSite_Librarian),
                new("Staff",      WebSiteRoles.WebSite_Staff),
                new("Member",     WebSiteRoles.WebSite_Member)
            };

            vm.UserStatusList = Enum.GetValues(typeof(UserStatus))
                .Cast<UserStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = (vm.UserStatus == e)
                });

            return View(vm);
        }

        // POST: /Admin/ApplicationUser/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // rebuild dropdowns because model binding lost them
                vm.RoleList = BuildRoleList();
                vm.UserStatusList = Enum.GetValues(typeof(UserStatus))
                .Cast<UserStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = (vm.UserStatus == e)
                });

                return View(vm);
            }

            try
            {
                await _userService.UpdateApplicationUserAsync(vm);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();             // someone deleted it in the meantime
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "The record was modified by another admin. Try again.");
                vm.RoleList = BuildRoleList();
                vm.UserStatusList = Enum.GetValues(typeof(UserStatus))
                .Cast<UserStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(),
                    Selected = (vm.UserStatus == e)
                });

                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/ApplicationUser/ToggleStatus/123
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var newStatus = user.UserStatus == UserStatus.Active ? UserStatus.Inactive : UserStatus.Active;
            await _userService.SetUserStatusAsync(id, newStatus);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/ApplicationUser/AssignRole/123
        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var vm = await _userService.GetByIdAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        // POST: /Admin/ApplicationUser/AssignRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string id, string roleName)
        {
            await _userService.AssignRoleAsync(id, roleName);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/ApplicationUser/Create
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ApplicationUserViewModel
            {
                RoleList = BuildRoleList(),
                UserStatusList = Enum.GetValues(typeof(UserStatus))
                    .Cast<UserStatus>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    })
            };

            ModelState.Clear();
            Console.WriteLine("Email in VM: " + vm.Email);
            return View(vm);
        }

        // POST: /Admin/ApplicationUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUserViewModel model, string password)
        {
            if (!ModelState.IsValid)
            {
                model.RoleList = BuildRoleList();
                model.UserStatusList = Enum.GetValues(typeof(UserStatus))
                    .Cast<UserStatus>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString(),
                        Selected = (model.UserStatus == e)
                    });

                //return View(model);
            }

            var result = await _userService.InsertApplicationUserAsync(model, password);

            

            return RedirectToAction(nameof(Index));
            
        }

        private static List<SelectListItem> BuildRoleList() => new()
        {
            new("Admin",      WebSiteRoles.WebSite_Admin),
            new("Librarian",  WebSiteRoles.WebSite_Librarian),
            new("Staff",      WebSiteRoles.WebSite_Staff),
            new("Member",     WebSiteRoles.WebSite_Member)
        };

    }
}
