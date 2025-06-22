using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public abstract class LibraryItemViewModel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Title { get; set; }
        public DateTime YearPublished { get; set; }
        public ItemType ItemType { get; set; }
        public string ShelfLocation { get; set; }
        public int LanguageId { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public string Description { get; set; }

        protected LibraryItemViewModel()
        {
        }

        public abstract LibraryItem ToDomainModel();

        public LibraryItemViewModel ConvertViewModel(LibraryItem model)
        {
            return model switch
            {
                Book book => new BookViewModel
                {
                    Id = book.Id,
                    ItemCode = book.ItemCode,
                    Title = book.Title,
                    YearPublished = book.YearPublished,
                    ItemType = ItemType.Book,
                    ShelfLocation = book.ShelfLocation,
                    LanguageId = book.LanguageId,
                    CategoryId = book.CategoryId,
                    PublisherId = book.CategoryId,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    Edition = book.Edition,
                    GenreId = book.GenreId
                },
                Newspaper newspaper => new NewspaperViewModel
                {
                    Id = newspaper.Id,
                    ItemCode = newspaper.ItemCode,
                    Title = newspaper.Title,
                    YearPublished = newspaper.YearPublished,
                    ItemType = ItemType.Newspaper,
                    ShelfLocation = newspaper.ShelfLocation,
                    LanguageId = newspaper.LanguageId,
                    CategoryId = newspaper.CategoryId,
                    PublisherId = newspaper.CategoryId,
                    Description = newspaper.Description,
                    ISSN = newspaper.ISSN,
                    IssuedDate = newspaper.IssuedDate,
                    IssueNumber = newspaper.IssueNumber
                },
                Journal journal => new JournalViewModel
                {
                    Id = journal.Id,
                    ItemCode = journal.ItemCode,
                    Title = journal.Title,
                    YearPublished = journal.YearPublished,
                    ItemType = ItemType.Journal,
                    ShelfLocation = journal.ShelfLocation,
                    LanguageId = journal.LanguageId,
                    CategoryId = journal.CategoryId,
                    PublisherId = journal.CategoryId,
                    Description = journal.Description,
                    ISSN = journal.ISSN,
                    Volume = journal.Volume,
                    Issue = journal.Issue,
                    Field = journal.Field
                },
                Periodical periodical => new PeriodicalViewModel
                {
                    Id = periodical.Id,
                    ItemCode = periodical.ItemCode,
                    Title = periodical.Title,
                    YearPublished = periodical.YearPublished,
                    ItemType = ItemType.Periodical,
                    ShelfLocation = periodical.ShelfLocation,
                    LanguageId = periodical.LanguageId,
                    CategoryId = periodical.CategoryId,
                    PublisherId = periodical.CategoryId,
                    Description = periodical.Description,
                    ISSN = periodical.ISSN,
                    Frequency = periodical.Frequency.ToString(),
                    Theme = periodical.Theme
                }

            };
        }

        protected LibraryItemViewModel(LibraryItem model)
        {
            Id = model.Id;
            ItemCode = model.ItemCode;
            Title = model.Title;
            YearPublished = model.YearPublished;
            ItemType = model.ItemType;
            ShelfLocation = model.ShelfLocation;
            LanguageId = model.LanguageId;
            CategoryId = model.CategoryId;
            PublisherId = model.PublisherId;
            Description = model.Description;
        }
        
    }
}
