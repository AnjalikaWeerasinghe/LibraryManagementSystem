using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class PeriodicalViewModel : LibraryItemViewModel
    {
        public string ISSN { get; set; }
        public string Frequency { get; set; }
        public string Theme { get; set; }

        public PeriodicalViewModel()
        {
        }

        public override LibraryItem ToDomainModel()
        {
            return new Periodical
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
                Frequency = Enum.Parse<Frequency>(this.Frequency),
                Theme = this.Theme
            };
        }
    }
}
