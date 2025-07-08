using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using static Library.ViewModels.PublisherViewModel;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class PublisherController : Controller
    {
        private IPublisherService _publisher;
        private IUnitOfWork _unitOfWork;

        public PublisherController(IPublisherService publisher, IUnitOfWork unitOfWork)
        {
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<PublisherViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _publisher.GetPublisherByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _publisher.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _publisher.GetPublisherById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PublisherViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var publisher = _unitOfWork.GenericRepository<Publisher>().GetById(vm.Id);
            if (publisher == null)
                return NotFound();

            publisher.Name = vm.Name;
            publisher.Address = vm.Address;
            publisher.PhoneNumber = vm.PhoneNumber;
            publisher.Landline = vm.Landline;

            _unitOfWork.GenericRepository<Publisher>().Update(publisher);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{publisher.Name}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PublisherViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                _unitOfWork.GenericRepository<Publisher>().Add(new Publisher
                {
                    Name = vm.Name,
                    Address = vm.Address,
                    PhoneNumber = vm.PhoneNumber,
                    Landline = vm.Landline
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
            var entity = _unitOfWork.GenericRepository<Publisher>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Publisher>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
