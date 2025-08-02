using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminDashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminDashboardViewModel> GetDashboardDataAsync()
        {
            var members = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().Where(u => u.UserRole == "Member");
            var staff = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().Where(u => u.UserRole == "Staff");
            var items = _unitOfWork.GenericRepository<LibraryItem>().GetAll().ToList();
            var borrowings = _unitOfWork.GenericRepository<Borrowing>().GetAll();
            var upcomingEvents = _unitOfWork.GenericRepository<LibraryEvent>()
                .GetAll()
                .Where(e => e.StartDate >= DateTime.Today)
                .OrderBy(e => e.StartDate)
                .Take(5)
                .Select(e => new RecentEventViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDate = e.StartDate
                }).ToList();

            var viewModel = new AdminDashboardViewModel
            {
                TotalMembers = members.Count(),
                TotalStaff = staff.Count(),
                TotalLibraryItems = items.Count(),
                TotalBorrowedItems = borrowings.Count(),
                UpcomingEventsCount = upcomingEvents.Count,
                UpcomingEvents = upcomingEvents,

                MonthlyBorrowings = borrowings
                .Where(b => b.BorrowedDate.Year == DateTime.Today.Year)
                .GroupBy(b => b.BorrowedDate.Month)
                .Select(g => new MonthlyBorrowingChartData
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    BorrowCount = g.Count()
                }).ToList(),

                LibraryItemDistribution = items
                .GroupBy(i => i.GetType().Name)
                .Select(g => new LibraryItemDistributionData
                {
                    ItemType = g.Key,
                    Count = g.Count()
                }).ToList(),

                OverdueReturnCount = borrowings.Count(b => b.DueDate < DateTime.Today && b.ReturnedDate == null)
            };
            return await Task.FromResult(viewModel);
        }
    }
}
