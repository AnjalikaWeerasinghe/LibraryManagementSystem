using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class BookViewModel : LibraryItemViewModel
    {
        public string ISBN { get; set; }
        public string Edition { get; set; }
        public int GenreId { get; set; }

        public BookViewModel()
        {
        }

        public override LibraryItem ToDomainModel()
        {
            return new Book
            {
                Id = this.Id,
                ItemCode = this.ItemCode,
                Title = this.Title,
                YearPublished = this.YearPublished,
                ShelfLocation = this.ShelfLocation,
                LanguageId = this.LanguageId,
                CategoryId = this.CategoryId,
                PublisherId = this.CategoryId,
                Description = this.Description,
                ISBN = this.ISBN,
                Edition = this.Edition,
                GenreId = this.GenreId
            };
        }

        public BookViewModel(Book book) : base(book)
        {
            ISBN = book.ISBN;
            Edition = book.Edition;
            GenreId = book.GenreId;
        }
    }
}
