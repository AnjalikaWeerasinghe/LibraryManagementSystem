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
    public class CountryService : ICountryService
    {
        private IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteCountry(int id)
        {
            var model = _unitOfWork.GenericRepository<Country>().GetById(id);
            _unitOfWork.GenericRepository<Country>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<CountryViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new CountryViewModel();
            int totalCount;
            List<CountryViewModel> vmList = new List<CountryViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Country>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Country>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<CountryViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<CountryViewModel> ConvertModelToViewModelList(List<Country> modelList)
        {
            return modelList.Select(x => new CountryViewModel(x)).ToList();
        }

        public CountryViewModel GetCountryById(int CountryId)
        {
            var model = _unitOfWork.GenericRepository<Country>().GetById(CountryId);
            var vm = new CountryViewModel(model);
            return vm;
        }

        public void InsertCountry(CountryViewModel country)
        {
            var model = new CountryViewModel().ConvertViewModel(country);
            _unitOfWork.GenericRepository<Country>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateCountry(CountryViewModel country)
        {
            var model = new CountryViewModel().ConvertViewModel(country);
            var ModelById = _unitOfWork.GenericRepository<Country>().GetById(model.Id);

            ModelById.Name = country.Name;

            _unitOfWork.GenericRepository<Country>().Update(ModelById);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<CountryViewModel>> GetAllAsync()
        {
            var countries = await _unitOfWork.GenericRepository<Country>().GetAllAsync();     

            return countries.Select(l => new CountryViewModel
            {
                Id = l.Id,
                Name = l.Name
            });
        }
    }
}
