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
    public class ApplicationUserService : IApplicationUserService
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<ApplicationUserViewModel> ConvertModelToViewModelList(List<ApplicationUser> modelList)
        {
            return modelList.Select(x => new ApplicationUserViewModel(x)).ToList();
        }

        public PagedResult<ApplicationUserViewModel> GetAllMember(int pageNumber, int pageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<ApplicationUser>().GetAll(x => x.IsMember == true).
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<ApplicationUser>().GetAll(x => x.IsMember == true).ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public PagedResult<ApplicationUserViewModel> GetAllStaff(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ApplicationUserViewModel> SearchMember(int pageNumber, int pageSize, string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateApplicationUser(ApplicationUserViewModel user)
        {
            var model = new ApplicationUserViewModel().ConvertViewModelToModel(user);
            var ModelById = _unitOfWork.GenericRepository<ApplicationUser>().GetById(model.Id);

            ModelById.FullName = user.FullName;
            ModelById.CallingName = user.CallingName;
            ModelById.UserName = user.UserName;
            ModelById.UserCode = user.UserCode;
            ModelById.Gender = user.Gender;
            ModelById.Email = user.Email;
            ModelById.Address = user.Address;
            ModelById.DOB = user.DOB;
            ModelById.UserStatus = user.UserStatus;
            ModelById.PictureUrl = user.PictureUrl;
            ModelById.SelectedRole = user.SelectedRole;

            _unitOfWork.GenericRepository<ApplicationUser>().Update(ModelById);
            _unitOfWork.Save();
        }

        public void InsertApplicationUser(ApplicationUserViewModel user)
        {
            var model = new ApplicationUserViewModel().ConvertViewModelToModel(user);
            _unitOfWork.GenericRepository<ApplicationUser>().Add(model);
            _unitOfWork.Save();
        }

        public void DeleteApplicationUser(int id)
        {
            var model = _unitOfWork.GenericRepository<ApplicationUser>().GetById(id);
            _unitOfWork.GenericRepository<ApplicationUser>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<ApplicationUserViewModel> GetUserByUserCode(string usercode, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<ApplicationUser>()
                .GetAll()
                .Where(p => p.UserCode.Contains(usercode))
                .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new ApplicationUserViewModel
            {
                FullName = p.FullName,


            }).ToList();

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public ApplicationUserViewModel GetUserById(int userId)
        {
            var model = _unitOfWork.GenericRepository<ApplicationUser>().GetById(userId);
            var vm = new ApplicationUserViewModel(model);
            return vm;
        }
    }
}
