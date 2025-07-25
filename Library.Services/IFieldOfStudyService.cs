using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IFieldOfStudyService
    {
        PagedResult<FieldOfStudyViewModel> GetAll(int pageNumber, int pageSize);
        FieldOfStudyViewModel GetFieldById(int fieldId);
        Task<IEnumerable<FieldOfStudyViewModel>> GetAllAsync();
        void UpdateField(FieldOfStudyViewModel field);
        string InsertField(FieldOfStudyViewModel field);
        void DeleteField(int id);
    }
}
