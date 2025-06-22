using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ILibraryInfoService
    {
        PagedResult<LibraryInfoViewModel> GetAll(int pageNumber, int pageSize);
        LibraryInfoViewModel GetLibraryById(int LibraryId);
        void UpdateLibraryInfo(LibraryInfoViewModel libraryInfo);
        void InsertLibraryInfo(LibraryInfoViewModel libraryInfo);
        void DeleteLibraryInfo(int id);

    }
}
