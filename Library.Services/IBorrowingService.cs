using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IBorrowingService
    {

        PagedResult<BorrowingViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<BorrowingViewModel> GetBorrowingsByUser(string userId, int pageNumber, int pageSize);
        BorrowingViewModel GetBorrowingById(int borrowingId);
        string UpdateBorrowing(BorrowingViewModel borrowing);
        void DeleteBorrowing(int id);
        string InsertBorrowing(BorrowingViewModel borrowing);

        Task<string> ReturnItemAsync(int borrowingId);
    }
}
