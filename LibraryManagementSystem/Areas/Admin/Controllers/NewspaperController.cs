using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NewspaperController : Controller
    {
        private readonly INewspaperService _newspaper;
        private IUnitOfWork _unitOfWork;

        public NewspaperController(INewspaperService newspaper, IUnitOfWork unitOfWork)
        {
            _newspaper = newspaper;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_newspaper.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = _newspaper.GetNewspaperById(id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewspaperViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var newspaper = _unitOfWork.GenericRepository<Newspaper>().GetById(vm.Id);
            if (newspaper == null)
                return NotFound();

            newspaper.Title = vm.Title;
            newspaper.Description = vm.Description;
            newspaper.ISSN = vm.ISSN;
            newspaper.ItemCode = vm.ItemCode;
            newspaper.CategoryId = vm.CategoryId;
            newspaper.PublishedYear = vm.PublishedYear;
            newspaper.PublisherId = vm.PublisherId;
            newspaper.LanguageId = vm.LanguageId;
            newspaper.IssuedDate = vm.IssuedDate;
            newspaper.ShelfLocation = vm.ShelfLocation;
            newspaper.IssueNumber = vm.IssueNumber;

            _unitOfWork.GenericRepository<Newspaper>().Update(newspaper);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{newspaper.Title}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewspaperViewModel vm)
        {
            _newspaper.InsertNewspaper(vm);
            TempData["SuccessMessage"] = $"{vm.Title} successfully added!";

            return View(new NewspaperViewModel());

        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<Newspaper>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Newspaper>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
