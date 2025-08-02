using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BorrowingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteBorrowing(int id)
        {
            var borrowing = _unitOfWork.GenericRepository<Borrowing>().GetById(id);
            var itemCopy = _unitOfWork.GenericRepository<ItemCopy>().GetById(borrowing.ItemCopyId);
            itemCopy.Available = true;

            _unitOfWork.GenericRepository<Borrowing>().Delete(borrowing);
            _unitOfWork.GenericRepository<ItemCopy>().Update(itemCopy);
            _unitOfWork.SaveAsync();
        }

        public PagedResult<BorrowingViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new BorrowingViewModel();
            int totalCount;
            List<BorrowingViewModel> vmList = new List<BorrowingViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Borrowing>()
                    .GetAll(includeProperties: "ApplicationUser,ItemCopy.LibraryItem")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Borrowing>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<BorrowingViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<BorrowingViewModel> ConvertModelToViewModelList(List<Borrowing> modelList)
        {
            return modelList.Select(x => new BorrowingViewModel(x)).ToList();
        }

        public BorrowingViewModel GetBorrowingById(int borrowingId)
        {
            var model = _unitOfWork.GenericRepository<Borrowing>()
                .GetAll(includeProperties: "ApplicationUser,ItemCopy.LibraryItem")
                .FirstOrDefault(x => x.Id == borrowingId);
            var vm = new BorrowingViewModel(model);
            return vm;
        }

        public string InsertBorrowing(BorrowingViewModel borrowing)
        {
            var itemCopy = _unitOfWork.GenericRepository<ItemCopy>().GetById(borrowing.ItemCopyId);
            if (itemCopy == null)
                return "Invalid ItemCopy selected.";

            if (!itemCopy.Available)
                return "This item is not available.";

            var borrowingitem = new Borrowing
            {
                UserId = borrowing.UserId,
                
                ItemCopyId = borrowing.ItemCopyId,
                BorrowedDate = borrowing.BorrowedDate,
                DueDate = borrowing.DueDate,
                ReturnedDate = borrowing.ReturnedDate,
                BorrowedStatus = borrowing.BorrowedStatus
            };

            itemCopy.Available = false;

            _unitOfWork.GenericRepository<Borrowing>().Add(borrowingitem);
            _unitOfWork.GenericRepository<ItemCopy>().Update(itemCopy);
            _unitOfWork.Save();

            return "Borrowing created successfully.";
        }

        public string UpdateBorrowing(BorrowingViewModel borrowing)
        {
            var borrowingitem = _unitOfWork.GenericRepository<Borrowing>().GetById(borrowing.Id);
            if (borrowingitem == null)
                return "Borrowing record not found.";

            borrowingitem.ReturnedDate = borrowing.ReturnedDate;
            borrowingitem.BorrowedStatus = borrowing.BorrowedStatus;

            if (borrowingitem.ReturnedDate != null)
            {
                var itemCopy = _unitOfWork.GenericRepository<ItemCopy>().GetById(borrowing.ItemCopyId);
                if (itemCopy != null)
                {
                    itemCopy.Available = true;
                    _unitOfWork.GenericRepository<ItemCopy>().Update(itemCopy);
                }
            }

            _unitOfWork.GenericRepository<Borrowing>().Update(borrowingitem);
            _unitOfWork.Save();

            return "Borrowing updated.";
        }

        public async Task<string> ReturnItemAsync(int borrowingId)
        {
            var borrowing = _unitOfWork.GenericRepository<Borrowing>()
                .GetAll(includeProperties: "ItemCopy")
                .FirstOrDefault(b => b.Id == borrowingId);

            if (borrowing == null || borrowing.ReturnedDate != null)
                return "Invalid or already returned borrowing record.";

            borrowing.ReturnedDate = DateTime.Now;

            if (DateTime.Now > borrowing.DueDate)
            {
                borrowing.BorrowedStatus = BorrowedStatus.Overdue;
                var daysLate = (DateTime.Now - borrowing.DueDate).Days;

                var fine = new Fine
                {
                    BorrowingId = borrowing.Id,
                    Amount = daysLate * 10, //  Rs. 10 per day
                    IssuedDate = DateTime.Now,
                    IsPaid = false
                };

                _unitOfWork.GenericRepository<Fine>().Add(fine);
            }
            else
            {
                borrowing.BorrowedStatus = BorrowedStatus.Returned;
            }

            borrowing.ItemCopy.Available = true;

            _unitOfWork.GenericRepository<Borrowing>().Update(borrowing);
            await _unitOfWork.SaveAsync();

            return "Book returned successfully.";
        }

        public PagedResult<BorrowingViewModel> GetBorrowingsByUser(string userId, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            var borrowings = _unitOfWork.GenericRepository<Borrowing>()
                .GetAll(b => b.UserId == userId, includeProperties: "ItemCopy.LibraryItem")
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            int total = _unitOfWork.GenericRepository<Borrowing>()
                .GetAll(b => b.UserId == userId)
                .Count();

            var vmList = borrowings.Select(b => new BorrowingViewModel(b)).ToList();

            return new PagedResult<BorrowingViewModel>
            {
                Data = vmList,
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}
