using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ICountryService
    {
        PagedResult<CountryViewModel> GetAll(int pageNumber, int pageSize);
        CountryViewModel GetCountryById(int CountryId);
        Task<IEnumerable<CountryViewModel>> GetAllAsync();
        void UpdateCountry(CountryViewModel country);
        string InsertCountry(CountryViewModel country);
        void DeleteCountry(int id);
    }
}
