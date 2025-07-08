using Microsoft.EntityFrameworkCore;
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

        public PagedResult<BookViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount;

                int excludeRecords = (pageNumber - 1) * pageSize;

                var books = _unitOfWork.GenericRepository<LibraryItem>()
                    .GetAll(
                        filter: item => item is Book,
                        includeProperties: "Language,Category,Publisher,Genre"
                        )
                    .Cast<Book>()
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .ToList();

                totalCount = _unitOfWork.GenericRepository<LibraryItem>()
                    .GetAll()
                    .OfType<Book>()
                    .Count();

                 var vmList = ConvertModelToBookViewModelList(books);

            return new PagedResult<BookViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private List<BookViewModel> ConvertModelToBookViewModelList(List<Book> books)
        {
            return books.Select(b => new BookViewModel
            {
                Id = b.Id,
                ItemCode = b.ItemCode,
                Title = b.Title,
                YearPublished = b.YearPublished,
                ShelfLocation = b.ShelfLocation,
                Language = b.Language.Name,
                Category = b.Category.Name,
                Publisher = b.Publisher.Name,
                Description = b.Description,

                ISBN = b.ISBN,
                Edition = b.Edition,
                Genre = b.Genre.Name
            }).ToList();
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
                case Newspaper newspaper:
                    viewModel = new NewspaperViewModel(newspaper);
                    break;
                case Journal journal:
                    viewModel = new JournalViewModel(journal);
                    break;
                case Periodical periodical:
                    viewModel = new PeriodicalViewModel(periodical);
                    break;
                default:
                    throw new InvalidOperationException("Invalid Library Item Type");
            }

            return viewModel;
        }

        public async Task InsertLibraryItem(LibraryItemViewModel libraryItem)
        {
            var category = _unitOfWork.GenericRepository<Category>()
                .GetAll(C => C.Name == libraryItem.Category)
                .FirstOrDefault();

            var language = _unitOfWork.GenericRepository<Language>()
                .GetAll(lan => lan.Name == libraryItem.Language)
                .FirstOrDefault();

            var publisher = _unitOfWork.GenericRepository<Publisher>()
                .GetAll(p => p.Name == libraryItem.Publisher)
                .FirstOrDefault();

            var genre = _unitOfWork.GenericRepository<Genre>()
                .GetAll(g => g.Name == libraryItem.Genre)
                .FirstOrDefault();

            LibraryItem newitem;

            switch (libraryItem)
            {
                case BookViewModel bookitem:
                    newitem = new Book
                    {
                        ItemCode = bookitem.ItemCode,
                        Title = bookitem.Title,
                        ISBN = bookitem.ISBN,
                        YearPublished = bookitem.YearPublished,
                        Edition = bookitem.Edition,
                        PublisherId = bookitem.PublisherId,
                        Description = bookitem.Description,
                        GenreId = bookitem.GenreId,
                        LanguageId = bookitem.LanguageId,
                        CategoryId = bookitem.CategoryId,
                        ShelfLocation = bookitem.ShelfLocation
                    };
                    break;

                case NewspaperViewModel newspaperitem:
                    newitem = new Newspaper
                    {
                        Title = newspaperitem.Title,
                        ItemCode = newspaperitem.ItemCode,
                        YearPublished = newspaperitem.YearPublished,
                        ShelfLocation = newspaperitem.ShelfLocation,
                        LanguageId = newspaperitem.LanguageId,
                        CategoryId = newspaperitem.CategoryId,
                        PublisherId = newspaperitem.PublisherId,
                        Description = newspaperitem.Description,

                        ISSN = newspaperitem.ISSN,
                        IssuedDate = newspaperitem.IssuedDate,
                        IssueNumber = newspaperitem.IssueNumber
                    };
                    break;

                case JournalViewModel journalitem:
                    newitem = new Journal
                    {
                        Title = journalitem.Title,
                        ItemCode = journalitem.ItemCode,
                        YearPublished = journalitem.YearPublished,
                        ShelfLocation = journalitem.ShelfLocation,
                        LanguageId = journalitem.LanguageId,
                        CategoryId = journalitem.CategoryId,
                        PublisherId = journalitem.PublisherId,
                        Description = journalitem.Description,

                        ISSN = journalitem.ISSN,
                        Volume = journalitem.Volume,
                        Issue = journalitem.Issue,
                        Field = journalitem.Field
                    };
                    break;

                case PeriodicalViewModel periodicalitem:
                    newitem = new Periodical
                    {
                        Title = periodicalitem.Title,
                        ItemCode = periodicalitem.ItemCode,
                        YearPublished = periodicalitem.YearPublished,
                        ShelfLocation = periodicalitem.ShelfLocation,
                        LanguageId = periodicalitem.LanguageId,
                        CategoryId = periodicalitem.CategoryId,
                        PublisherId = periodicalitem.PublisherId,
                        Description = periodicalitem.Description,

                        ISSN = periodicalitem.ISSN,
                        Frequency = Enum.Parse<Frequency>(periodicalitem.Frequency),
                        Theme = periodicalitem.Theme
                    };
                    break;

                default:
                    throw new InvalidOperationException("Unsupported Item Type");
            }

            _unitOfWork.GenericRepository<LibraryItem>().Add(newitem);
            _unitOfWork.Save();

        }

        public async Task UpdateLibraryItemAsync(LibraryItemViewModel libraryItem)
        {
            var category = _unitOfWork.GenericRepository<Category>()
                .GetAll(C => C.Name == libraryItem.Category)
                .FirstOrDefault();

            var language = _unitOfWork.GenericRepository<Language>()
                .GetAll(lan => lan.Name == libraryItem.Language)
                .FirstOrDefault();

            var publisher = _unitOfWork.GenericRepository<Publisher>()
                .GetAll(p => p.Name == libraryItem.Publisher)
                .FirstOrDefault();

            LibraryItem item;

            switch (libraryItem)
            {
                case BookViewModel bookitem:
                    var book = _unitOfWork.GenericRepository<Book>().GetById(bookitem.Id);
                    if (book == null)
                    {
                        throw new Exception("Book Not Found");
                    }

                    book.Title = bookitem.Title;
                    book.ItemCode = bookitem.ItemCode;
                    book.YearPublished = bookitem.YearPublished;
                    book.ShelfLocation = bookitem.ShelfLocation;
                    book.LanguageId = bookitem.LanguageId;
                    book.CategoryId = bookitem.CategoryId;
                    book.PublisherId = bookitem.PublisherId;
                    book.Description = bookitem.Description;

                    book.ISBN = bookitem.ISBN;
                    book.Edition = bookitem.Edition;
                    book.GenreId = bookitem.GenreId;

                    item = book;
                    break;

                case NewspaperViewModel newspaperitem:
                    var newspaper = _unitOfWork.GenericRepository<Newspaper>().GetById(newspaperitem.Id);
                    if (newspaper == null)
                    {
                        throw new Exception("Newspaper Not Found");
                    }

                    newspaper.Title = newspaperitem.Title;
                    newspaper.ItemCode = newspaperitem.ItemCode;
                    newspaper.YearPublished = newspaperitem.YearPublished;
                    newspaper.ShelfLocation = newspaperitem.ShelfLocation;
                    newspaper.LanguageId = newspaperitem.LanguageId;
                    newspaper.CategoryId = newspaperitem.CategoryId;
                    newspaper.PublisherId = newspaperitem.PublisherId;
                    newspaper.Description = newspaperitem.Description;

                    newspaper.ISSN = newspaperitem.ISSN;
                    newspaper.IssuedDate = newspaperitem.IssuedDate;
                    newspaper.IssueNumber = newspaperitem.IssueNumber;

                    item = newspaper;
                    break;

                case JournalViewModel journalitem:
                    var journal = _unitOfWork.GenericRepository<Journal>().GetById(journalitem.Id);
                    if (journal == null)
                    {
                        throw new Exception("Journal Not Found");
                    }

                    journal.Title = journalitem.Title;
                    journal.ItemCode = journalitem.ItemCode;
                    journal.YearPublished = journalitem.YearPublished;
                    journal.ShelfLocation = journalitem.ShelfLocation;
                    journal.LanguageId = journalitem.LanguageId;
                    journal.CategoryId = journalitem.CategoryId;
                    journal.PublisherId = journalitem.PublisherId;
                    journal.Description = journalitem.Description;

                    journal.ISSN = journalitem.ISSN;
                    journal.Volume = journalitem.Volume;
                    journal.Issue = journalitem.Issue;
                    journal.Field = journalitem.Field;

                    item = journal;
                    break;

                case PeriodicalViewModel periodicalitem:
                    var periodical = _unitOfWork.GenericRepository<Periodical>().GetById(periodicalitem.Id);
                    if (periodical == null)
                    {
                        throw new Exception("Periodical Not Found");
                    }

                    periodical.Title = periodicalitem.Title;
                    periodical.ItemCode = periodicalitem.ItemCode;
                    periodical.YearPublished = periodicalitem.YearPublished;
                    periodical.ShelfLocation = periodicalitem.ShelfLocation;
                    periodical.LanguageId = periodicalitem.LanguageId;
                    periodical.CategoryId = periodicalitem.CategoryId;
                    periodical.PublisherId = periodicalitem.PublisherId;
                    periodical.Description = periodicalitem.Description;

                    periodical.ISSN = periodicalitem.ISSN;
                    periodical.Frequency = Enum.Parse<Frequency>(periodicalitem.Frequency);
                    periodical.Theme = periodicalitem.Theme;

                    item = periodical;
                    break;

                default:
                    throw new InvalidOperationException("Invalid Item Type");
            }

            _unitOfWork.GenericRepository<LibraryItem>().Update(item);
            _unitOfWork.Save();
        }
    }
}
