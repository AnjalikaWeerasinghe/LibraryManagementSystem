using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteBook(int id)
        {
            var model = _unitOfWork.GenericRepository<Book>().GetById(id);
            _unitOfWork.GenericRepository<Book>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<BookViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new BookViewModel();
            int totalCount;
            List<BookViewModel> vmList = new List<BookViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Book>()
                    .GetAll(includeProperties:"Language,Category,Publisher,Genre")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Book>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<BookViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<BookViewModel> ConvertModelToViewModelList(List<Book> modelList)
        {
            return modelList.Select(x => new BookViewModel(x)).ToList();
        }

        public BookViewModel GetBookById(int bookId)
        {
            var model = _unitOfWork.GenericRepository<Book>().GetById(bookId);
            var vm = new BookViewModel(model);
            return vm;
        }

        public async Task InsertBookAsync(BookViewModel book)
        {
            var entity = new BookViewModel().ConvertToViewModelToModel(book);
            _unitOfWork.GenericRepository<Book>().Add(entity);
            await _unitOfWork.SaveAsync();
        }


        public void UpdateBook(BookViewModel book)
        {
            var model = new BookViewModel().ConvertToViewModelToModel(book);
            var ModelById = _unitOfWork.GenericRepository<Book>().GetById(model.Id);

            ModelById.ItemCode = book.ItemCode;
            ModelById.Title = book.Title;
            ModelById.Description = book.Description;
            ModelById.PublishedYear = book.PublishedYear;
            ModelById.ShelfLocation = book.ShelfLocation;
            ModelById.Edition = book.Edition;
            ModelById.LanguageId = book.LanguageId;
            ModelById.PublisherId = book.PublisherId;
            ModelById.CategoryId = book.CategoryId;
            ModelById.GenreId = book.GenreId;
            ModelById.ISBN = book.ISBN;

            _unitOfWork.GenericRepository<Book>().Update(ModelById);
            _unitOfWork.Save();
        }
    }
}
