using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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

            var vm = new CategoryViewModel
            {
                PagedCategories = _category.GetAll(pageNumber, pageSize),
                ItemTypeList = Enum.GetValues(typeof(ItemType))
                    .Cast<ItemType>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    })
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateorUpdate(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PagedCategories = _category.GetAll(1, 10); 
                vm.ItemTypeList = Enum.GetValues(typeof(ItemType))
                    .Cast<ItemType>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    });
            }

            if (vm.Id == 0)
            {
                var result = _category.InsertCategory(vm);
                if (result == "Category already exists.")
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
                _category.UpdateCategory(vm);
                TempData["SuccessMessage"] = $"{vm.Name} updated successfully !";
                ModelState.Clear();
            }
                
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _category.GetCategoryById(id);
            if (category == null)
                return NotFound();

            var pagedData = _category.GetAll(1, 10);

            var vm = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ItemType = category.ItemType,
                ItemTypeList = Enum.GetValues(typeof(ItemType))
                    .Cast<ItemType>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString(),
                        Selected = (e == category.ItemType)
                    }),
                PagedCategories = pagedData
            };

            return View("Index", vm);
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
