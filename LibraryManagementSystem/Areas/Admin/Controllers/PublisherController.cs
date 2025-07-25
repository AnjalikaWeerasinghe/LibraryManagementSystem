using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using static Library.ViewModels.PublisherViewModel;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var result = string.IsNullOrWhiteSpace(searchTerm)
                ? _publisher.GetAll(pageNumber, pageSize)
                : _publisher.GetPublisherByName(searchTerm, pageNumber, pageSize);

            var vm = new PublisherViewModel
            {
                PagedPublishers = result,
                
            };

            ViewBag.SearchTerm = searchTerm;
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateorUpdate(PublisherViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PagedPublishers = _publisher.GetAll(1, 10);
                return View(vm);
                
            }

            if (vm.Id == 0)
            {
                var result = _publisher.InsertPublisher(vm);
                if (result == "Publisher already exists.")
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
                _publisher.UpdatePublisher(vm);
                TempData["SuccessMessage"] = $"{vm.Name} updated successfully !";
                ModelState.Clear();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var publisher = _publisher.GetPublisherById(id);
            if (publisher == null)
                return NotFound();

            var pagedData = _publisher.GetAll(1, 10);

            var vm = new PublisherViewModel
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Address = publisher.Address,
                PhoneNumber = publisher.PhoneNumber,
                Landline = publisher.Landline,
                PagedPublishers = pagedData
            };

            return View("Index", vm);
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
