using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IGenreService
    {
        PagedResult<GenreViewModel> GetAll(int pageNumber, int pageSize);
        GenreViewModel GetGenreById(int GenreId);
        Task<IEnumerable<GenreViewModel>> GetAllAsync();
        void UpdateGenre(GenreViewModel genre);
        string InsertGenre(GenreViewModel genre);
        void DeleteGenre(int id);
    }
}
