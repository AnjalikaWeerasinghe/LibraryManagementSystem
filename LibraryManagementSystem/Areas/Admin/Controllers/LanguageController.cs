using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var vm = new LanguageViewModel
            {
                PagedLanguages = _language.GetAll(pageNumber, pageSize) 
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateorUpdate(LanguageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PagedLanguages = _language.GetAll(1, 10);
            }

            if (vm.Id == 0)
            {
                var result = _language.InsertLanguage(vm);
                if (result == "Language already exists.")
                {
                    TempData["ErrorMessage"] = result;
                }
                else
                {
                    TempData["SuccessMessage"] = $"{vm.Name} successfully Added!";
                }

                ModelState.Clear();
            }
            else
            {
                _language.UpdateLanguage(vm);
                TempData["SuccessMessage"] = $"{vm.Name} updated successfully !";
                ModelState.Clear();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var language = _language.GetLanguageById(id);
            if (language == null)
                return NotFound();

            var pagedData = _language.GetAll(1, 10);

            var vm = new LanguageViewModel
            {
                Id = language.Id,
                Name = language.Name,
                PagedLanguages = pagedData
            };

            return View("Index", vm);
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
