using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeContentController : Controller
    {
        private IHomeContentService _home;
        private IUnitOfWork _unitOfWork;

        public HomeContentController(IHomeContentService home, IUnitOfWork unitOfWork)
        {
            _home = home;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var content = _unitOfWork.GenericRepository<HomeContent>().GetAll().FirstOrDefault();
            return View(content);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var content = _unitOfWork.GenericRepository<HomeContent>().GetById(id ?? 1);
            if (content == null)
                return NotFound();

            return View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeContentViewModel content)
        {
            try
            {
                _home.UpdateContent(content); // Use service

                ViewBag.UpdateSuccess = $"'{content.Title}' updated successfully!";
                ViewBag.ShowModal = true;
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while updating.");
            }

            return View(content);
        }
    }
}
