using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            return View(_fieldOfStudyService.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FieldOfStudyViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _unitOfWork.GenericRepository<FieldOfStudy>().Add(new FieldOfStudy
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _fieldOfStudyService.GetFieldById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(FieldOfStudyViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var field = _unitOfWork.GenericRepository<FieldOfStudy>().GetById(vm.Id);
            if (field == null)
                return NotFound();

            field.Name = vm.Name;

            _unitOfWork.GenericRepository<FieldOfStudy>().Update(field);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{field.Name}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
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
