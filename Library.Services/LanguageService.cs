using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class LanguageService : ILanguageService
    {
        private IUnitOfWork _unitOfWork;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteLanguage(int id)
        {
            var model = _unitOfWork.GenericRepository<Language>().GetById(id);
            _unitOfWork.GenericRepository<Language>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<LanguageViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new LanguageViewModel();
            int totalCount;
            List<LanguageViewModel> vmList = new List<LanguageViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Language>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Language>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<LanguageViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<LanguageViewModel> ConvertModelToViewModelList(List<Language> modelList)
        {
            return modelList.Select(x => new LanguageViewModel(x)).ToList();
        }

        public LanguageViewModel GetLanguageById(int LanguageId)
        {
            var model = _unitOfWork.GenericRepository<Language>().GetById(LanguageId);
            var vm = new LanguageViewModel(model);
            return vm;
        }

        public string InsertLanguage(LanguageViewModel language)
        {
            var existing = _unitOfWork.GenericRepository<Language>()
                    .GetAll()
                    .FirstOrDefault(c => c.Name == language.Name);

            if (existing != null)
                return "Language already exists.";

            var model = new Language
            {
                Name = language.Name,
                
            };

            _unitOfWork.GenericRepository<Language>().Add(model);
            _unitOfWork.Save();

            return "Language added successfully.";
        }

        public void UpdateLanguage(LanguageViewModel language)
        {
            var model = new LanguageViewModel().ConvertViewModel(language);
            var ModelById = _unitOfWork.GenericRepository<Language>().GetById(model.Id);

            ModelById.Name = language.Name;

            _unitOfWork.GenericRepository<Language>().Update(ModelById);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<LanguageViewModel>> GetAllAsync()
        {
            var langs = await _unitOfWork.GenericRepository<Language>().GetAllAsync();     // <- this is the missing call

            return langs.Select(l => new LanguageViewModel
            {
                Id = l.Id,
                Name = l.Name
            });
        }
    }
}
