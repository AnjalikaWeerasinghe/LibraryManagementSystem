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
    public class LibraryItemService : ILibraryItemService
    {
        private IUnitOfWork _unitOfWork;

        public LibraryItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteLibraryItem(int id)
        {
            var model = _unitOfWork.GenericRepository<LibraryItem>().GetById(id);
            _unitOfWork.GenericRepository<LibraryItem>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<LibraryItemViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount;
            List<LibraryItemViewModel> vmList;

            try
            {
                int excludeRecords = (pageNumber - 1) * pageSize;

                var modelList = _unitOfWork.GenericRepository<LibraryItem>()
                    .GetAll()
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .ToList();

                totalCount = _unitOfWork.GenericRepository<LibraryItem>()
                    .GetAll()
                    .Count();

                 vmList = ConvertModelToViewModelList(modelList);
            }
            catch(Exception)
            {
                throw;
            }

            return new PagedResult<LibraryItemViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private List<LibraryItemViewModel> ConvertModelToViewModelList(List<LibraryItem> modelList)
        {
            var vmList = new List<LibraryItemViewModel>();

            foreach (var item in modelList)
            {
                LibraryItemViewModel viewModel;

                switch (item)
                {
                    case Book book:
                        viewModel = new BookViewModel(book);
                        break;

                    default:
                        throw new InvalidOperationException("Invalid LibraryItem Type.");
                }

                vmList.Add(viewModel);
            }
            return vmList;
        }

        public async Task<LibraryItemViewModel> GetLibraryItemByIdAsync(int id)
        {
            var libraryitem = _unitOfWork.GenericRepository<LibraryItem>().GetById(id);

            if (libraryitem == null)
            {
                return null;
            }

            LibraryItemViewModel viewModel;

            switch (libraryitem)
            {
                case Book book:
                    viewModel = new BookViewModel(book);
                    break;
                default:
                    throw new InvalidOperationException("Invalid Library Item Type");
            }

            return viewModel;
        }

        public async Task InsertLibraryItem(LibraryItem libraryItem)
        {
            LibraryItem item;

            switch (libraryItem.ItemType)
            {
                case ItemType.Book:
                    item = new Book
                    {
                        Title = libraryItem.Title,
                        ItemCode = libraryItem.ItemCode
                    };
                    break;

                default:
                    throw new InvalidOperationException("Unsupported Item Type");
            }

            _unitOfWork.GenericRepository<LibraryItem>().Add(item);
            _unitOfWork.Save();

        }

        public async Task UpdateLibraryItem(LibraryItem libraryItem)
        {
            var existingItem = _unitOfWork.GenericRepository<LibraryItem>().GetById(libraryItem.Id);

            if (existingItem == null)
            {
                throw new Exception("Item Not Found");
            }

            existingItem.Title = libraryItem.Title;

            switch (existingItem)
            {
                case Book book:
                    break;
                case Newspaper newspaper:
                    break;
            }

            _unitOfWork.GenericRepository<LibraryItem>().Update(existingItem);
            _unitOfWork.Save();
        }
    }
}
