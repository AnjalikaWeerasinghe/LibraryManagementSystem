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
        PagedResult<BookViewModel> GetAllBooks(int pageNumber, int pageSize);
        Task<LibraryItemViewModel> GetLibraryItemByIdAsync(int ItemID);
        Task InsertLibraryItem(LibraryItemViewModel libraryItem);
        Task UpdateLibraryItemAsync(LibraryItemViewModel libraryItem);
        Task DeleteLibraryItem(int id);

    }
}
