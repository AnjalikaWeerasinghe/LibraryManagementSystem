using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BorrowingController : Controller
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IApplicationUserService _userService;
        private readonly IItemCopyService _itemCopyService;
        private readonly IUnitOfWork _unitOfWork;

        public BorrowingController(IBorrowingService borrowingService, IApplicationUserService userService, 
            IUnitOfWork unitOfWork, IItemCopyService itemCopyService)
        {
            _borrowingService = borrowingService;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _itemCopyService = itemCopyService; 
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = _borrowingService.GetAll(pageNumber, pageSize);
            var viewModel = new BorrowingViewModel
            {
                PagedBorrowings = pagedResult,
                Users = await _userService.GetAllMembersAsSelectListAsync(), 
                AvailableItemCopies = await _itemCopyService.GetAvailableItemCopiesAsSelectListAsync()
            };
            return View(pagedResult);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new BorrowingViewModel
            {
                Users = await _userService.GetAllMembersAsSelectListAsync(),
                AvailableItemCopies = _unitOfWork.GenericRepository<ItemCopy>()
                    .GetAll(i => i.Available, includeProperties: "LibraryItem")
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.LibraryItem.Title + " - " + i.Id
                    })
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BorrowingViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Users = await _userService.GetAllMembersAsSelectListAsync();
                vm.AvailableItemCopies = _unitOfWork.GenericRepository<ItemCopy>()
                    .GetAll(includeProperties: "LibraryItem")
                    .Where(i => i.Available)
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.LibraryItem.Title + " - " + i.ItemCopyCode
                    });
                //return View(vm);
            }

            var result = _borrowingService.InsertBorrowing(vm);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm =  _borrowingService.GetBorrowingById(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BorrowingViewModel vm)
        {
            var result = _borrowingService.UpdateBorrowing(vm);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _borrowingService.DeleteBorrowing(id);
            TempData["SuccessMessage"] = "Borrowing record is deleted.";
            return RedirectToAction(nameof(Index));
        }

    }
}
