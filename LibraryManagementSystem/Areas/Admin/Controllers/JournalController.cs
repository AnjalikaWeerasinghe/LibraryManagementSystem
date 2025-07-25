using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Threading.Tasks;

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
        private IFieldOfStudyService _fieldOfStudyService;

        public JournalController(IJournalService journal, IUnitOfWork unitOfWork, 
            ILanguageService languageService, IPublisherService publisherService, ICategoryService categoryService, 
            IFieldOfStudyService ofStudyService)
        {
            _journal = journal;
            _unitOfWork = unitOfWork;
            _languageService = languageService;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _fieldOfStudyService = ofStudyService;
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
        public async Task<IActionResult> Edit(int id)
        {
            var journal = _journal.GetJournalById(id);
            if (journal == null) return NotFound();  

            var vm = new JournalViewModel
            {
                Id = journal.Id,
                Title = journal.Title,
                ISSN = journal.ISSN,
                PublishedYear = journal.PublishedYear,
                LanguageId = journal.LanguageId,
                CategoryId = journal.CategoryId,
                PublisherId = journal.PublisherId,
                Description = journal.Description,
                Volume = journal.Volume,
                Issue = journal.Issue,
                FieldOfStudyId = journal.FieldOfStudyId,
                ItemCode = journal.ItemCode,

                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = (await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Journal).ToList(),
                Fields = await _fieldOfStudyService.GetAllAsync()

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JournalViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = await _languageService.GetAllAsync();
                vm.Publishers = await _publisherService.GetAllAsync();
                vm.Categories =(await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Journal).ToList();
                vm.Fields = await _fieldOfStudyService.GetAllAsync();

            }

            var journal = _unitOfWork.GenericRepository<Journal>().GetById(vm.Id);
            if (journal == null)
                return NotFound();

            journal.Id = vm.Id;
            journal.Title = vm.Title;
            journal.ItemCode = vm.ItemCode;
            journal.PublishedYear = vm.PublishedYear;
            journal.LanguageId = vm.LanguageId;
            journal.CategoryId = vm.CategoryId;
            journal.PublisherId = vm.PublisherId;
            journal.Description = vm.Description;
            journal.ISSN = vm.ISSN;
            journal.Volume = vm.Volume;
            journal.Issue = vm.Issue;
            journal.FieldOfStudyId = vm.FieldOfStudyId;

            _unitOfWork.GenericRepository<Journal>().Update(journal);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = $"{vm.Title} updated successfully !";

            return RedirectToAction(nameof(Index));
        }
    

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new JournalViewModel
            {
                ItemCode = _journal.GenerateNextJournalCode(),
                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Fields = await _fieldOfStudyService.GetAllAsync()   
            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(JournalViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
            }

            vm.ItemCode = _journal.GenerateNextJournalCode();

            string result = _journal.InsertJournal(vm);

            if (result == "Journal already exists.")
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
            var entity = _unitOfWork.GenericRepository<Journal>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Journal>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }

        private async Task PopulateDropdowns(JournalViewModel vm)
        {
            vm.Languages = await _languageService.GetAllAsync();
            vm.Publishers = await _publisherService.GetAllAsync();
            vm.Categories = await _categoryService.GetAllAsync();
            vm.Fields = await _fieldOfStudyService.GetAllAsync();

        }
    }
}