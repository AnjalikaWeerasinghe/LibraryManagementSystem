using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JournalController : Controller
    {
        private IJournalService _journal;
        private ILanguageService _languageService;
        private IUnitOfWork _unitOfWork;
        private IPublisherService _publisherService;
        private ICategoryService _categoryService;

        public JournalController(IJournalService journal, IUnitOfWork unitOfWork, 
            ILanguageService languageService, IPublisherService publisherService, ICategoryService categoryService)
        {
            _journal = journal;
            _unitOfWork = unitOfWork;
            _languageService = languageService;
            _publisherService = publisherService;
            _categoryService = categoryService;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<JournalViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _journal.GetJournalByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _journal.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _journal.GetJournalById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(JournalViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var journal = _unitOfWork.GenericRepository<Journal>().GetById(vm.Id);
            if (journal == null)
                return NotFound();

            journal.ItemCode = vm.ItemCode;
            journal.Title = vm.Title;
            journal.ShelfLocation = vm.ShelfLocation;
            journal.PublishedYear = vm.PublishedYear;
            journal.LanguageId = vm.LanguageId;
            journal.CategoryId = vm.CategoryId;
            journal.PublisherId = vm.PublisherId;
            journal.Description = vm.Description;
            journal.ISSN = vm.ISSN;
            journal.Volume = vm.Volume;
            journal.Issue = vm.Issue;
            journal.Field = vm.Field;

            _unitOfWork.GenericRepository<Journal>().Update(journal);
            _unitOfWork.Save();

            ViewBag.UpdateSuccess = $" '{journal.Title}' updated successfully!";
            ViewBag.ShowModal = true;

            return View(vm);
        }
    

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new JournalViewModel
            {
                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync()

            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(JournalViewModel vm)
        {
            //vm.ItemCode = _journal.GenerateNextJournalCode();

            _journal.InsertJournal(vm);
            TempData["SuccessMessage"] = $"{vm.Title} successfully added!";

            // PRG: redirect to avoid duplicate submissions
            return RedirectToAction(nameof(Create));

            

        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.GenericRepository<Journal>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Journal>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }
    }
}