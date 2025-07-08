using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
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
            return View(_country.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _country.GetCountryById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CountryViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var country = _unitOfWork.GenericRepository<Country>().GetById(vm.Id);
            if (country == null)
                return NotFound();

            country.Name = vm.Name;

            _unitOfWork.GenericRepository<Country>().Update(country);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{country.Name}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CountryViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _unitOfWork.GenericRepository<Country>().Add(new Country
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
            var entity = _unitOfWork.GenericRepository<Country>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Country>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }

    }
}
