using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class NewspaperService : INewspaperService
    {
        private IUnitOfWork _unitOfWork;

        public NewspaperService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteNewspaper(int id)
        {
            var model = _unitOfWork.GenericRepository<Newspaper>().GetById(id);
            _unitOfWork.GenericRepository<Newspaper>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<NewspaperViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new NewspaperViewModel();
            int totalCount;
            List<NewspaperViewModel> vmList = new List<NewspaperViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Newspaper>()
                    .GetAll(includeProperties: "Language,Category,Publisher")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Newspaper>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<NewspaperViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<NewspaperViewModel> ConvertModelToViewModelList(List<Newspaper> modelList)
        {
            return modelList.Select(x => new NewspaperViewModel(x)).ToList();
        }

        public NewspaperViewModel GetNewspaperById(int newspaperId)
        {
            var model = _unitOfWork.GenericRepository<Newspaper>().GetById(newspaperId);
            var vm = new NewspaperViewModel(model);
            return vm;
        }

        public void InsertNewspaper(NewspaperViewModel newspaper)
        {
            var model = new NewspaperViewModel().ConvertToViewModelToModel(newspaper);
            _unitOfWork.GenericRepository<Newspaper>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateNewspaper(NewspaperViewModel newspaper)
        {
            var model = new NewspaperViewModel().ConvertToViewModelToModel(newspaper);
            var ModelById = _unitOfWork.GenericRepository<Newspaper>().GetById(model.Id);

            ModelById.ItemCode = newspaper.ItemCode;
            ModelById.Title = newspaper.Title;
            ModelById.Description = newspaper.Description;
            ModelById.PublishedYear = newspaper.PublishedYear;
            ModelById.ShelfLocation = newspaper.ShelfLocation;
            ModelById.IssuedDate = newspaper.IssuedDate;
            ModelById.IssueNumber = newspaper.IssueNumber;
            ModelById.LanguageId = newspaper.LanguageId;
            ModelById.PublisherId = newspaper.PublisherId;
            ModelById.CategoryId = newspaper.CategoryId;
            ModelById.ISSN = newspaper.ISSN;

            _unitOfWork.GenericRepository<Newspaper>().Update(ModelById);
            _unitOfWork.Save();
        }
    }
}
