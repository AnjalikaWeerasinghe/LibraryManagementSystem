using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class GenreService : IGenreService
    {
        private IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteGenre(int id)
        {
            var model = _unitOfWork.GenericRepository<Genre>().GetById(id);
            _unitOfWork.GenericRepository<Genre>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<GenreViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new GenreViewModel();
            int totalCount;
            List<GenreViewModel> vmList = new List<GenreViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Genre>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Genre>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<GenreViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<GenreViewModel> ConvertModelToViewModelList(List<Genre> modelList)
        {
            return modelList.Select(x => new GenreViewModel(x)).ToList();
        }

        public GenreViewModel GetGenreById(int GenreId)
        {
            var model = _unitOfWork.GenericRepository<Genre>().GetById(GenreId);
            var vm = new GenreViewModel(model);
            return vm;
        }

        public string InsertGenre(GenreViewModel genre)
        {
            var existing = _unitOfWork.GenericRepository<Genre>()
                    .GetAll()
                    .FirstOrDefault(c => c.Name == genre.Name);

            if (existing != null)
                return "Genre already exists.";

            var model = new Genre
            {
                Name = genre.Name,
                Description = genre.Description
            };

            _unitOfWork.GenericRepository<Genre>().Add(model);
            _unitOfWork.Save();

            return "Genre added successfully.";
        }

        public void UpdateGenre(GenreViewModel genre)
        {
            
            var ModelById = _unitOfWork.GenericRepository<Genre>().GetById(genre.Id);

            ModelById.Name = genre.Name;
            ModelById.Description = genre.Description;

            _unitOfWork.GenericRepository<Genre>().Update(ModelById);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<GenreViewModel>> GetAllAsync()
        {
            var genres = await _unitOfWork.GenericRepository<Genre>().GetAllAsync();

            return genres.Select(l => new GenreViewModel
            {
                Id = l.Id,
                Name = l.Name
            });
        }
    }
}
