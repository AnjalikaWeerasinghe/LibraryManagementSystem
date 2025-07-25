using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class FieldOfStudyService : IFieldOfStudyService
    {
        private  IUnitOfWork _unitOfWork;
        public FieldOfStudyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteField(int id)
        {
            var model = _unitOfWork.GenericRepository<FieldOfStudy>().GetById(id);
            _unitOfWork.GenericRepository<FieldOfStudy>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<FieldOfStudyViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new FieldOfStudyViewModel();
            int totalCount;
            List<FieldOfStudyViewModel> vmList = new List<FieldOfStudyViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<FieldOfStudy>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<FieldOfStudy>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<FieldOfStudyViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<FieldOfStudyViewModel> ConvertModelToViewModelList(List<FieldOfStudy> modelList)
        {
            return modelList.Select(x => new FieldOfStudyViewModel(x)).ToList();
        }

        public async Task<IEnumerable<FieldOfStudyViewModel>> GetAllAsync()
        {
            var fields = await _unitOfWork.GenericRepository<FieldOfStudy>().GetAllAsync();

            return fields.Select(f => new FieldOfStudyViewModel
            {
                Id = f.Id,
                Name = f.Name
            });
        }

        public FieldOfStudyViewModel GetFieldById(int fieldId)
        {
            var model = _unitOfWork.GenericRepository<FieldOfStudy>().GetById(fieldId);
            var vm = new FieldOfStudyViewModel(model);
            return vm;
        }

        public string InsertField(FieldOfStudyViewModel field)
        {
            var existing = _unitOfWork.GenericRepository<FieldOfStudy>()
                    .GetAll()
                    .FirstOrDefault(c => c.Name == field.Name);

            if (existing != null)
                return "Field already exists.";

            var model = new FieldOfStudy
            {
                Name = field.Name,
               
            };
            _unitOfWork.GenericRepository<FieldOfStudy>().Add(model);
            _unitOfWork.Save();

            return "Field added successfully.";
        }

        public void UpdateField(FieldOfStudyViewModel field)
        {
            
            var ModelById = _unitOfWork.GenericRepository<FieldOfStudy>().GetById(field.Id);

            ModelById.Name = field.Name;

            _unitOfWork.GenericRepository<FieldOfStudy>().Update(ModelById);
            _unitOfWork.Save();
        }
    }
}
