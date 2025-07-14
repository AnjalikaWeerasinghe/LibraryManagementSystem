using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            // Populate ItemTypeList from enum
            viewModel.ItemTypeList = Enum.GetValues(typeof(ItemType))
                .Cast<ItemType>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString(), 
                    Selected = (viewModel.ItemType == e)
                });

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel vm)
        {

                var category = _unitOfWork.GenericRepository<Category>().GetById(vm.Id);
                if (category == null)
                    return NotFound();

                category.Name = vm.Name;
                category.ItemType = vm.ItemType;

                _unitOfWork.GenericRepository<Category>().Update(category);
                _unitOfWork.Save();

                // Optional: Reload ViewModel with dropdown for confirmation page
                vm.ItemTypeList = Enum.GetValues(typeof(ItemType))
                    .Cast<ItemType>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString(),
                        Selected = (vm.ItemType == e)
                    });

                ViewBag.UpdateSuccess = $" '{category.Name}' updated successfully!";
                ViewBag.ShowModal = true;

                return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CategoryViewModel
            {
                ItemTypeList = Enum.GetValues(typeof(ItemType))
                    .Cast<ItemType>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel vm)
        {

            _category.InsertCategory(vm);
            TempData["SuccessMessage"] = $"{vm.Name} successfully added!";

            // PRG: redirect to avoid duplicate submissions
            return RedirectToAction(nameof(Create));

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
