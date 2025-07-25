using Library.Models;
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
        Task<IEnumerable<LanguageViewModel>> GetAllAsync();
        void UpdateLanguage(LanguageViewModel language);
        string InsertLanguage(LanguageViewModel language);
        void DeleteLanguage(int id);

    }
}
