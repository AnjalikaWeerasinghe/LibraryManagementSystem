using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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

            var result = string.IsNullOrWhiteSpace(searchTerm)
                ? _author.GetAll(pageNumber, pageSize)
                : _author.GetAuthorByName(searchTerm, pageNumber, pageSize);

            var vm = new AuthorViewModel
            {
                PagedAuthors = result,
                Countries = _author.GetAllCountries()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
            };

            ViewBag.SearchTerm = searchTerm;
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateorUpdate(AuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PagedAuthors = _author.GetAll(1, 10);
                vm.Countries = _author.GetAllCountries()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
            }

            if (vm.Id == 0)
            {
                var result = _author.InsertAuthor(vm);
                if (result == "Author already exists.")
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
                _author.UpdateAuthor(vm);
                TempData["SuccessMessage"] = $"{vm.Name} updated successfully !";
                ModelState.Clear();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var author = _author.GetAuthorById(id);
            if (author == null)
                return NotFound();

            var pagedData = _author.GetAll(1, 10);
            var countries = _author.GetAllCountries()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

            var vm = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                CountryId = author.CountryId,
                Countries = countries,

                PagedAuthors = pagedData
            };

            return View("Index", vm);
        }


        public IActionResult Delete(int id)
        {

            var entity = _unitOfWork.GenericRepository<Author>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Author>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
