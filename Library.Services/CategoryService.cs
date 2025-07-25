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
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteCategory(int id)
        {
            var model = _unitOfWork.GenericRepository<Category>().GetById(id);
            _unitOfWork.GenericRepository<Category>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<CategoryViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new CategoryViewModel();
            int totalCount;
            List<CategoryViewModel> vmList = new List<CategoryViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Category>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Category>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<CategoryViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<CategoryViewModel> ConvertModelToViewModelList(List<Category> modelList)
        {
            return modelList.Select(x => new CategoryViewModel(x)).ToList();
        }

        public CategoryViewModel GetCategoryById(int CategoryId)
        {
            var model = _unitOfWork.GenericRepository<Category>().GetById(CategoryId);
            var vm = new CategoryViewModel(model);
            return vm;
        }

        public string InsertCategory(CategoryViewModel category)
        {
            var existing = _unitOfWork.GenericRepository<Category>()
                    .GetAll()
                    .FirstOrDefault(c => c.Name == category.Name && c.ItemType == category.ItemType);

            if (existing != null)
                return "Category already exists.";

            var model = new Category
            {
                Name = category.Name,
                ItemType = category.ItemType
            };

            _unitOfWork.GenericRepository<Category>().Add(model);
            _unitOfWork.Save();

            return "Category added successfully.";
        }

        public void UpdateCategory(CategoryViewModel category)
        {
            
            var ModelById = _unitOfWork.GenericRepository<Category>().GetById(category.Id);

            ModelById.Name = category.Name;
            ModelById.ItemType = category.ItemType;

            _unitOfWork.GenericRepository<Category>().Update(ModelById);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var cats = await _unitOfWork.GenericRepository<Category>().GetAllAsync();     

            return cats.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ItemType = c.ItemType
            });
        }
    }
}
