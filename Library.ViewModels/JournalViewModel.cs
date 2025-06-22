using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class JournalViewModel : LibraryItemViewModel
    {
        public string ISSN { get; set; }
        public string Volume { get; set; }
        public string Issue { get; set; }
        public string Field { get; set; }

        public JournalViewModel()
        {
        }

        public override LibraryItem ToDomainModel()
        {
            return new Journal
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
                Volume = this.Volume,
                Issue = this.Issue,
                Field = this.Field
            };
        }
    }
}
