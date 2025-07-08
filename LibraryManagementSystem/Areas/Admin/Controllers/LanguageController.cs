using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LanguageController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ILanguageService _language;

        public LanguageController(ILanguageService language, IUnitOfWork unitOfWork)
        {
            _language = language;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_language.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _language.GetLanguageById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(LanguageViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var language = _unitOfWork.GenericRepository<Language>().GetById(vm.Id);
            if (language == null)
                return NotFound();

            language.Name = vm.Name;

            _unitOfWork.GenericRepository<Language>().Update(language);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{language.Name}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LanguageViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _unitOfWork.GenericRepository<Language>().Add(new Language
                {
                    Name = vm.Name
                });
                _unitOfWork.Save();

                TempData["SuccessMessage"] = $"{vm.Name} successfully Added !";
                ModelState.Clear(); // clear input fields
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while saving.");
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<Language>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Language>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
