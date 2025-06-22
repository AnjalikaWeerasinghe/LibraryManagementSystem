using Library.Models;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ILibraryItemService
    {
        PagedResult<LibraryItemViewModel> GetAll(int pageNumber, int pageSize);
        Task<LibraryItem> GetLibraryItemById(int ItemID);
        Task InsertLibraryItem(LibraryItem libraryItem);
        Task UpdateLibraryItem(LibraryItem libraryItem);
        Task DeleteLibraryItem(int id);
    }
}
