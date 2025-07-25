using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAuthorService
    {
        PagedResult<AuthorViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<AuthorViewModel> GetAuthorByName(string name, int pageNumber, int pageSize);
        AuthorViewModel GetAuthorById(int AuthorId);
        void UpdateAuthor(AuthorViewModel author);
        string InsertAuthor(AuthorViewModel author);
        void DeleteAuthor(int id);

        IEnumerable<CountryViewModel> GetAllCountries();
    }
}
