using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class PeriodicalController : Controller
    {
        private readonly IPeriodicalService _periodical;
        private IUnitOfWork _unitOfWork;

        public PeriodicalController(IPeriodicalService periodical, IUnitOfWork unitOfWork)
        {
            _periodical = periodical;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_periodical.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {

            var model = new PeriodicalViewModel
            {
                ItemCode = _periodical.GenerateNextPeriodicalCode()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeriodicalViewModel vm)
        {
            vm.ItemCode = _periodical.GenerateNextPeriodicalCode();
            if (!ModelState.IsValid)
            {
                // If model state is invalid, return the view with the current model
                return View(vm);
            }

            _periodical.InsertPeriodical(vm);
            TempData["SuccessMessage"] = $"{vm.Title} successfully added!";

            return View(new PeriodicalViewModel());

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
    }
}
