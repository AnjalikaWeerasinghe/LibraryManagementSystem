using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface INewspaperService
    {
        PagedResult<NewspaperViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<NewspaperViewModel> GetNewspaperByName(string name, int pageNumber, int pageSize);
        NewspaperViewModel GetNewspaperById(int newspaperId);
        void UpdateNewspaper(NewspaperViewModel newspaper);
        string InsertNewspaper(NewspaperViewModel newspaper);
        void DeleteNewspaper(int id);
        string GenerateNextNewspaperCode();
    }
}
