using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IJournalService
    {
        PagedResult<JournalViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<JournalViewModel> GetJournalByName(string name, int pageNumber, int pageSize);
        JournalViewModel GetJournalById(int journalId);
        void UpdateJournal(JournalViewModel journal);
        string InsertJournal(JournalViewModel journal);
        void DeleteJournal(int id);
        string GenerateNextJournalCode();
    }
}
