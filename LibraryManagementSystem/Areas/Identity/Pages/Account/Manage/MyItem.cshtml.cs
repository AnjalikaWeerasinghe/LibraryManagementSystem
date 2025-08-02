using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.ViewModels;
using Library.Utilities;
using System.Security.Claims;

namespace LibraryManagementSystem.Areas.Identity.Pages.Account.Manage
{
   
    public class MyItemModel : PageModel
    {
        private readonly IBorrowingService _borrowingService;

        public MyItemModel(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        public PagedResult<BorrowingViewModel> Borrowings { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;


        public void OnGet()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Borrowings = _borrowingService.GetBorrowingsByUser(userId, PageNumber, PageSize);
        }
    }
}
