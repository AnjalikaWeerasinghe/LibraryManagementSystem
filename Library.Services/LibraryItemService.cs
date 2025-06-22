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

        public Task DeleteLibraryItem(int id)
        {
            throw new NotImplementedException();
        }

        public PagedResult<LibraryItemViewModel> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<LibraryItem> GetLibraryItemById(int ItemID)
        {
            throw new NotImplementedException();
        }

        public Task InsertLibraryItem(LibraryItem libraryItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLibraryItem(LibraryItem libraryItem)
        {
            throw new NotImplementedException();
        }
    }
}
