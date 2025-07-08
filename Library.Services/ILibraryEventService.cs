using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ILibraryEventService
    {
        PagedResult<LibraryEventViewModel> GetAll(int pageNumber, int pageSize);
        LibraryEventViewModel GetLibraryEventById(int LibraryId);
        void UpdateLibraryEvent(LibraryEventViewModel libraryEvent);
        void InsertLibraryEvent(LibraryEventViewModel libraryEvent);
        void DeleteLibraryEvent(int id);

    }
}
