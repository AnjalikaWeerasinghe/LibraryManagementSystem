using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewspaperController : Controller
    {
        private INewspaperService _newspaper;
        private IUnitOfWork _unitOfWork;
        private ILanguageService _languageService;
        private IPublisherService _publisherService;
        private ICategoryService _categoryService;

        public NewspaperController(INewspaperService newspaper, IUnitOfWork unitOfWork, 
            ILanguageService languageService, IPublisherService publisherService, ICategoryService categoryService)
        {
            _newspaper = newspaper;
            _unitOfWork = unitOfWork;
            _languageService = languageService;
            _publisherService = publisherService;
            _categoryService = categoryService;
        }
        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<NewspaperViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _newspaper.GetNewspaperByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _newspaper.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var newspaper = _newspaper.GetNewspaperById(id);
            if (newspaper == null) return NotFound();

            var vm = new NewspaperViewModel
            {
                Id = newspaper.Id,
                Title = newspaper.Title,
                ISSN = newspaper.ISSN,
                IssuedDate = newspaper.IssuedDate,
                IssueNumber = newspaper.IssueNumber,
                LanguageId = newspaper.LanguageId,
                CategoryId = newspaper.CategoryId,
                PublisherId = newspaper.PublisherId,
                Description = newspaper.Description,
                ItemCode = newspaper.ItemCode,

                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = (await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Newspaper).ToList(),

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewspaperViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = await _languageService.GetAllAsync();
                vm.Publishers = await _publisherService.GetAllAsync();
                vm.Categories = (await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Newspaper).ToList();

            }

            var newspaper = _unitOfWork.GenericRepository<Newspaper>().GetById(vm.Id);
            if (newspaper == null)
                return NotFound();

            newspaper.Id = vm.Id;
            newspaper.Title = vm.Title;
            newspaper.ItemCode = vm.ItemCode;
            newspaper.LanguageId = vm.LanguageId;
            newspaper.CategoryId = vm.CategoryId;
            newspaper.PublisherId = vm.PublisherId;
            newspaper.Description = vm.Description;
            newspaper.ISSN = vm.ISSN;
            newspaper.IssuedDate = vm.IssuedDate;
            newspaper.IssueNumber = vm.IssueNumber;

            _unitOfWork.GenericRepository<Newspaper>().Update(newspaper);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = $"{vm.Title} updated successfully !";

            return RedirectToAction(nameof(Index)); ;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var vm = new NewspaperViewModel
            {
                ItemCode = _newspaper.GenerateNextNewspaperCode(),
                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync()

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewspaperViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
            }

            vm.ItemCode = _newspaper.GenerateNextNewspaperCode();

            string result = _newspaper.InsertNewspaper(vm);

            if (result == "Newspaper already exists.")
            {
                TempData["ErrorMessage"] = result;
            }
            else
            {
                TempData["SuccessMessage"] = $"{vm.Title} successfully added!";
            }

            return RedirectToAction(nameof(Create));

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

        private async Task PopulateDropdowns(NewspaperViewModel vm)
        {
            vm.Languages = await _languageService.GetAllAsync();
            vm.Publishers = await _publisherService.GetAllAsync();
            vm.Categories = await _categoryService.GetAllAsync();
            
        }
    }
}
