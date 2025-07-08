using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ILanguageService
    {
        PagedResult<LanguageViewModel> GetAll(int pageNumber, int pageSize);
        LanguageViewModel GetLanguageById(int LanguageId);
        void UpdateLanguage(LanguageViewModel language);
        void InsertLanguage(LanguageViewModel language);
        void DeleteLanguage(int id);

    }
}
