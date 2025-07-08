using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class NewspaperViewModel : LibraryItemViewModel
    {
        public string ISSN { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssueNumber { get; set; }

        public NewspaperViewModel()
        {
        }

        public override LibraryItem ToDomainModel()
        {
            return new Newspaper
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
                ISSN = this.ISSN,
                IssuedDate = this.IssuedDate,
                IssueNumber = this.IssueNumber
            };
        }

        public NewspaperViewModel(Newspaper newspaper) : base(newspaper)
        {
            ISSN = newspaper.ISSN;
            IssuedDate = newspaper.IssuedDate;
            IssueNumber = newspaper.IssueNumber;
        }
    }
}
