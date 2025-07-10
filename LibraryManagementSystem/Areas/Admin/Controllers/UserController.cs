using Library.Models;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UserController : Controller
    {
        private IApplicationUserService _userservice;

        public UserController(IApplicationUserService userservice)
        {
            _userservice = userservice;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_userservice.GetAll(pageNumber, pageSize));
        }

        public IActionResult AllMembers(int pageNumber = 1, int pageSize = 10)
        {
            return View(_userservice.GetAllMember(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _userservice.GetUserById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationUserViewModel vm)
        {
            _userservice.UpdateApplicationUser(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationUserViewModel vm)
        {
            _userservice.InsertApplicationUser(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _userservice.DeleteApplicationUser(id);
            return RedirectToAction("Index");
        }
    }
}
