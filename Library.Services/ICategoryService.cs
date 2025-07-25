using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ICategoryService
    {
        PagedResult<CategoryViewModel> GetAll(int pageNumber, int pageSize);
        CategoryViewModel GetCategoryById(int CategoryId);
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        void UpdateCategory(CategoryViewModel category);
        string InsertCategory(CategoryViewModel category);
        void DeleteCategory(int id);

    }
}
