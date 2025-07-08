using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ICategoryService _category;

        public CategoryController(ICategoryService category, IUnitOfWork unitOfWork)
        {
            _category = category;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_category.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _category.GetCategoryById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var category = _unitOfWork.GenericRepository<Category>().GetById(vm.Id);
            if (category == null)
                return NotFound();

            category.Name = vm.Name;

            _unitOfWork.GenericRepository<Category>().Update(category);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{category.Name}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _unitOfWork.GenericRepository<Category>().Add(new Category
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
            var entity = _unitOfWork.GenericRepository<Category>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Category>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
