using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FieldOfStudyController : Controller
    {
        private IFieldOfStudyService _fieldOfStudyService;
        private IUnitOfWork _unitOfWork;

        public FieldOfStudyController(IFieldOfStudyService fieldOfStudyService, IUnitOfWork unitOfWork)
        {
            _fieldOfStudyService = fieldOfStudyService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var vm = new FieldOfStudyViewModel
            {
                PagedFields = _fieldOfStudyService.GetAll(pageNumber, pageSize)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateorUpdate(FieldOfStudyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PagedFields = _fieldOfStudyService.GetAll(1, 10);
                
            }

            if (vm.Id == 0)
            {
                var result = _fieldOfStudyService.InsertField(vm);
                if (result == "Field already exists.")
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
                _fieldOfStudyService.UpdateField(vm);
                TempData["SuccessMessage"] = $"{vm.Name} updated successfully !";
                ModelState.Clear();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var field = _fieldOfStudyService.GetFieldById(id);
            if (field == null)
                return NotFound();

            var pagedData = _fieldOfStudyService.GetAll(1, 10);

            var vm = new FieldOfStudyViewModel
            {
                Id = field.Id,
                Name = field.Name,
                PagedFields = pagedData
            };

            return View("Index", vm);
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<FieldOfStudy>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<FieldOfStudy>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
