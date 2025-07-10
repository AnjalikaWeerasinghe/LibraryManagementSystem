using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IBookService
    {
        PagedResult<BookViewModel> GetAll(int pageNumber, int pageSize);
        BookViewModel GetBookById(int bookId);
        void UpdateBook(BookViewModel book);
        void InsertBook(BookViewModel book);
        void DeleteBook(int id);
    }
}
