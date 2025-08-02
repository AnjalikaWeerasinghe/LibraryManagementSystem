using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalMembers { get; set; }
        public int TotalStaff { get; set; }
        public int TotalLibraryItems { get; set; }
        public int TotalBorrowedItems { get; set; }
        public int UpcomingEventsCount { get; set; }
        public int OverdueReturnCount { get; set; }

        public List<RecentEventViewModel> UpcomingEvents { get; set; }
        public List<MonthlyBorrowingChartData> MonthlyBorrowings { get; set; } = new();
        public List<LibraryItemDistributionData> LibraryItemDistribution { get; set; } = new();
    }

    public class RecentEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class MonthlyBorrowingChartData
    {
        public string Month { get; set; }
        public int BorrowCount { get; set; }
    }

    public class LibraryItemDistributionData
    {
        public string ItemType { get; set; }
        public int Count { get; set; }
    }
}
