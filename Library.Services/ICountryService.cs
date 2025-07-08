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
        void UpdateCountry(CountryViewModel country);
        void InsertCountry(CountryViewModel country);
        void DeleteCountry(int id);
    }
}
