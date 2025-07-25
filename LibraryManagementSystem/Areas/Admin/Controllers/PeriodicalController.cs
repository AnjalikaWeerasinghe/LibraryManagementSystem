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
    public class PeriodicalController : Controller
    {
        private IPeriodicalService _periodical;
        private IUnitOfWork _unitOfWork;
        private ILanguageService _languageService;
        private IPublisherService _publisherService;
        private ICategoryService _categoryService;

        public PeriodicalController(IPeriodicalService periodical, IUnitOfWork unitOfWork, IPublisherService publisherService, 
            ICategoryService categoryService, ILanguageService languageService)
        {
            _periodical = periodical;
            _unitOfWork = unitOfWork;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _languageService = languageService;
        }

        public IActionResult Index(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            PagedResult<PeriodicalViewModel> result;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                result = _periodical.GetPeriodicalByName(searchTerm, pageNumber, pageSize);
            }
            else
            {
                result = _periodical.GetAll(pageNumber, pageSize);
            }

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var model = new PeriodicalViewModel
            {
                ItemCode = _periodical.GenerateNextPeriodicalCode(),
                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),

                FrequencyList = Enum.GetValues(typeof(Frequency))
                    .Cast<Frequency>()
                    .Select(f => new SelectListItem
                    {
                        Value = f.ToString(),
                        Text = f.ToString()
                    })

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeriodicalViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
            }

            vm.ItemCode = _periodical.GenerateNextPeriodicalCode();

            string result = _periodical.InsertPeriodical(vm);

            if (result == "Periodical already exists.")
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
            var entity = _unitOfWork.GenericRepository<Periodical>().GetById(id);
            if (entity == null)
                return NotFound();

            _unitOfWork.GenericRepository<Periodical>().Delete(entity);
            _unitOfWork.Save();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var periodical = _periodical.GetPeriodicalById(id);
            if (periodical == null) return NotFound();

            var vm = new PeriodicalViewModel
            {
                Id = periodical.Id,
                Title = periodical.Title,
                ISSN = periodical.ISSN,
                PublishedYear = periodical.PublishedYear,
                LanguageId = periodical.LanguageId,
                CategoryId = periodical.CategoryId,
                PublisherId = periodical.PublisherId,
                Description = periodical.Description,
                Theme = periodical.Theme,
                ItemCode = periodical.ItemCode,
                Frequency = periodical.Frequency,

                Languages = await _languageService.GetAllAsync(),
                Publishers = await _publisherService.GetAllAsync(),
                Categories = (await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Periodical).ToList(),

                FrequencyList = Enum.GetValues(typeof(Frequency))
                .Cast<Frequency>()
                .Select(f => new SelectListItem
                {
                    Value = f.ToString(),
                    Text = f.ToString(),
                    Selected = (f == periodical.Frequency)
                })
            };
    
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PeriodicalViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Languages = await _languageService.GetAllAsync();
                vm.Publishers = await _publisherService.GetAllAsync();
                vm.Categories = (await _categoryService.GetAllAsync())
                    .Where(c => c.ItemType == ItemType.Periodical).ToList();

            }

            var periodical = _unitOfWork.GenericRepository<Periodical>().GetById(vm.Id);
            if (periodical == null)
                return NotFound();

            periodical.Id = vm.Id;
            periodical.Title = vm.Title;
            periodical.ItemCode = vm.ItemCode;
            periodical.PublishedYear = vm.PublishedYear;
            periodical.LanguageId = vm.LanguageId;
            periodical.CategoryId = vm.CategoryId;
            periodical.PublisherId = vm.PublisherId;
            periodical.Description = vm.Description;
            periodical.ISSN = vm.ISSN;
            periodical.Frequency = vm.Frequency;
            periodical.Theme = vm.Theme;

            _unitOfWork.GenericRepository<Periodical>().Update(periodical);
            _unitOfWork.Save();

            vm.FrequencyList = Enum.GetValues(typeof(Frequency))
                    .Cast<Frequency>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString(),
                        Selected = (vm.Frequency == e)
                    });

            TempData["SuccessMessage"] = $"{vm.Title} updated successfully !";

            return View(vm);
        }

        private async Task PopulateDropdowns(PeriodicalViewModel vm)
        {
            vm.Languages = await _languageService.GetAllAsync();
            vm.Publishers = await _publisherService.GetAllAsync();
            vm.Categories = await _categoryService.GetAllAsync();

        }
    }
}
