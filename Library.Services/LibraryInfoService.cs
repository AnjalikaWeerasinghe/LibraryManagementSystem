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
    public class LibraryInfoService : ILibraryInfoService
    {
        private IUnitOfWork _unitOfWork;

        public LibraryInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteLibraryInfo(int id)
        {
            var model = _unitOfWork.GenericRepository<LibraryInfo>().GetById(id);
            _unitOfWork.GenericRepository<LibraryInfo>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<LibraryInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new LibraryInfoViewModel();
            int totalCount;
            List<LibraryInfoViewModel> vmList = new List<LibraryInfoViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<LibraryInfo>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<LibraryInfo>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<LibraryInfoViewModel> 
            { 
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public LibraryInfoViewModel GetLibraryById(int LibraryId)
        {
            var model = _unitOfWork.GenericRepository<LibraryInfo>().GetById(LibraryId);
            var vm = new LibraryInfoViewModel(model);
            return vm;
        }

        public void InsertLibraryInfo(LibraryInfoViewModel libraryInfo)
        {
            var model = new LibraryInfoViewModel().ConvertViewModel(libraryInfo);
            _unitOfWork.GenericRepository<LibraryInfo>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateLibraryInfo(LibraryInfoViewModel libraryInfo)
        {
            var model = new LibraryInfoViewModel().ConvertViewModel(libraryInfo);
            var ModelById = _unitOfWork.GenericRepository<LibraryInfo>().GetById(model.Id);

            ModelById.RegisteredCode = libraryInfo.RegisteredCode;
            ModelById.Name = libraryInfo.Name;
            ModelById.Address = libraryInfo.Address;
            ModelById.PhoneNumber = libraryInfo.PhoneNumber;
            ModelById.Email = libraryInfo.Email;
            ModelById.OpeningHours = libraryInfo.OpeningHours;
            ModelById.OpeningDays = libraryInfo.OpeningDays;
            ModelById.Description = libraryInfo.Description;

            _unitOfWork.GenericRepository<LibraryInfo>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<LibraryInfoViewModel> ConvertModelToViewModelList(List<LibraryInfo> modelList)
        {
            return modelList.Select(x => new LibraryInfoViewModel(x)).ToList();
        }
    }
}
