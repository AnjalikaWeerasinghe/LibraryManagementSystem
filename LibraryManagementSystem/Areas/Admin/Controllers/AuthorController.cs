using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AuthorController : Controller
    {
        private IAuthorService _author;
        private IUnitOfWork _unitOfWork;
        private ICountryService _country;

        public AuthorController(IAuthorService author, IUnitOfWork unitOfWork, ICountryService country)
        {
            _author = author;
            _unitOfWork = unitOfWork;
            _country = country;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            //return View(_author.GetAll(pageNumber, pageSize));

            PagedResult<AuthorViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _author.GetAuthorByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _author.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        private List<SelectListItem> GetCountryList()
        {
            return _unitOfWork.GenericRepository<Country>()
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                }).ToList();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _author.GetAuthorById(id);
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AuthorViewModel vm)
        {
            //if (!ModelState.IsValid)
            //{
            //    vm.CountryList = GetCountryList(); // Repopulate on validation failure
            //    return View(vm);
            //}

            //try
            //{
            //    _author.UpdateAuthor(vm); // Use service

            //    ViewBag.UpdateSuccess = $"'{vm.Name}' updated successfully!";
            //    ViewBag.ShowModal = true;
            //}
            //catch
            //{
            //    ModelState.AddModelError("", "An error occurred while updating.");
            //}

            //vm.CountryList = GetCountryList();
            //return View(vm);

            _author.UpdateAuthor(vm);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var vm = new AuthorViewModel
            {
                Countries = await _country.GetAllAsync()
            };
            return View(vm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(AuthorViewModel vm)
        {
            

            _author.InsertAuthor(vm);
            TempData["SuccessMessage"] = $"{vm.Name} created successfully!";
            return RedirectToAction(nameof(Create));

        }

        public IActionResult Delete(int id)
        {
            //_author.DeleteAuthor(id);
            //return RedirectToAction("Index");

            var entity = _unitOfWork.GenericRepository<Author>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Author>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
