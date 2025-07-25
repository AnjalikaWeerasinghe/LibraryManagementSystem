using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ICountryService _country;

        public CountryController(ICountryService country, IUnitOfWork unitOfWork)
        {
            _country = country;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var vm = new CountryViewModel
            {
                PagedCountries = _country.GetAll(pageNumber, pageSize)
            };

            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var country = _country.GetCountryById(id);
            if (country == null)
                return NotFound();

            var pagedData = _country.GetAll(1, 10);

            var vm = new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name,
                PagedCountries = pagedData
            };

            return View("Index", vm);
        }

        [HttpPost]
        public IActionResult CreateorUpdate(CountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PagedCountries = _country.GetAll(1, 10);
                
            }

            if (vm.Id == 0)
            {
                var result = _country.InsertCountry(vm);
                if (result == "Genre already exists.")
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
                _country.UpdateCountry(vm);
                TempData["SuccessMessage"] = $"{vm.Name} updated successfully !";
                ModelState.Clear();
            }


            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<Country>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Country>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }

    }
}
