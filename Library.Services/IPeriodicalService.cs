using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IPeriodicalService
    {
        PagedResult<PeriodicalViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<PeriodicalViewModel> GetPeriodicalByName(string name, int pageNumber, int pageSize);
        PeriodicalViewModel GetPeriodicalById(int periodicalId);
        void UpdatePeriodical(PeriodicalViewModel periodical);
        string InsertPeriodical(PeriodicalViewModel periodical);
        void DeletePeriodical(int id);
        string GenerateNextPeriodicalCode();
    }
}
