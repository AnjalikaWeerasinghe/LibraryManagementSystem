using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services.Results;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            int totalCount;
            var vm = new BookViewModel();
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

        public InsertBookResult InsertBook(BookViewModel book)
        {

            var existingBook = _unitOfWork.GenericRepository<Book>()
                .GetAll()
                .FirstOrDefault(c => c.Title == book.Title && c.ISBN == book.ISBN);

            if (existingBook != null)
            {
                return new InsertBookResult
                {
                    Success = false,
                    Message = "Book already exists."
                };
            }

            string bookCode = GenerateNextBookCode();

            var newBook = new Book
            {
                Title = book.Title,
                ISBN = book.ISBN,
                Description = book.Description,
                PublisherId = book.PublisherId,
                LanguageId = book.LanguageId,
                CategoryId = book.CategoryId,
                GenreId = book.GenreId,
                PublishedYear = book.PublishedYear,
                Edition = book.Edition,
                ItemCode = bookCode,
            };

            _unitOfWork.GenericRepository<Book>().Add(newBook);
            _unitOfWork.Save();

            int numberOfCopies = book.NumberOfCopies > 0 ? book.NumberOfCopies : 1;

            for (int i = 1; i <= numberOfCopies; i++)
            {
                var itemCopy = new ItemCopy
                {
                    LibraryItemId = newBook.Id,
                    ShelfLocation = book.ShelfLocation ?? "Default Shelf",
                    Available = true,
                    ItemCopyCode = $"{bookCode}-{i}"
                };

                _unitOfWork.GenericRepository<ItemCopy>().Add(itemCopy);
            }

            _unitOfWork.Save();

            return new InsertBookResult
            {
                Success = true,
                BookId = newBook.Id,
                Message = $"{numberOfCopies} copy/copies added with code: {bookCode}"
            };
        }


        public void UpdateBook(BookViewModel book)
        {
            var ModelById = _unitOfWork.GenericRepository<Book>().GetById(book.Id);
            if (book == null)
                throw new KeyNotFoundException($"Book with Id {book.Id} not found.");

            ModelById.Title = book.Title;
            ModelById.Description = book.Description;
            ModelById.PublishedYear = book.PublishedYear;
            ModelById.Edition = book.Edition;
            ModelById.LanguageId = book.LanguageId;
            ModelById.PublisherId = book.PublisherId;
            ModelById.CategoryId = book.CategoryId;
            ModelById.GenreId = book.GenreId;
            ModelById.ISBN = book.ISBN;

            _unitOfWork.GenericRepository<Book>().Update(ModelById);
            _unitOfWork.Save();
        }

        public PagedResult<BookViewModel> GetBookByName(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Book>()
                 .GetAll(includeProperties: "Language,Category,Publisher,Genre")
                 .Where(p => p.Title.Contains(name))
                 .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new BookViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ItemCode = p.ItemCode,
                ISBN = p.ISBN,
                PublishedYear = p.PublishedYear,
                Edition = p.Edition,
                LanguageId = p.LanguageId,
                CategoryId = p.CategoryId,
                PublisherId = p.PublisherId,
                GenreId = p.GenreId,
                Description = p.Description,

            }).ToList();

            return new PagedResult<BookViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public string GenerateNextBookCode()
        {
            var lastbook = _unitOfWork.GenericRepository<Book>()
           .GetAll()
           .OrderByDescending(e => e.Id)
           .FirstOrDefault();

            int lastNumber = 0;

            if (lastbook != null && Regex.IsMatch(lastbook.ItemCode ?? "", @"^ITD-BK-(\d{5})$"))
            {
                var match = Regex.Match(lastbook.ItemCode, @"^ITD-BK-(\d{5})$");
                lastNumber = int.Parse(match.Groups[1].Value);
            }

            return $"ITD-BK-{(lastNumber + 1).ToString("D5")}";
        }
    }
}
