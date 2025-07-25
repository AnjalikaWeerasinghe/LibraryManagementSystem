using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class PublisherService : IPublisherService
    {
        private IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeletePublisher(int id)
        {
            var model = _unitOfWork.GenericRepository<Publisher>().GetById(id);
            _unitOfWork.GenericRepository<Publisher>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<PublisherViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new PublisherViewModel();
            int totalCount;
            List<PublisherViewModel> vmList = new List<PublisherViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Publisher>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Publisher>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<PublisherViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<PublisherViewModel> ConvertModelToViewModelList(List<Publisher> modelList)
        {
            return modelList.Select(x => new PublisherViewModel(x)).ToList();
        }

        public PublisherViewModel GetPublisherById(int PublisherId)
        {
            var model = _unitOfWork.GenericRepository<Publisher>().GetById(PublisherId);
            var vm = new PublisherViewModel(model);
            return vm;
        }

        public string InsertPublisher(PublisherViewModel publisher)
        {
            var existing = _unitOfWork.GenericRepository<Publisher>()
                    .GetAll()
                    .FirstOrDefault(c => c.Name == publisher.Name );

            if (existing != null)
                return "Publisher already exists.";

            var model = new Publisher
            {
                Name = publisher.Name,
                Address = publisher.Address,
                PhoneNumber = publisher.PhoneNumber,
                Landline = publisher.Landline
            };

            _unitOfWork.GenericRepository<Publisher>().Add(model);
            _unitOfWork.Save();

            return "Publisher added successfully.";
        }

        public void UpdatePublisher(PublisherViewModel publisher)
        {
            
            var ModelById = _unitOfWork.GenericRepository<Publisher>().GetById(publisher.Id);

            ModelById.Name = publisher.Name;
            ModelById.Address = publisher.Address;
            ModelById.PhoneNumber = publisher.PhoneNumber;
            ModelById.Landline = publisher.Landline;

            _unitOfWork.GenericRepository<Publisher>().Update(ModelById);
            _unitOfWork.Save();
        }

        public PagedResult<PublisherViewModel> GetPublisherByName(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Publisher>()
                .GetAll()
                .Where(p => p.Name.Contains(name))
                .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new PublisherViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
                Landline = p.Landline
            }).ToList();

            return new PagedResult<PublisherViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<PublisherViewModel>> GetAllAsync()
        {
            var publishers = await _unitOfWork.GenericRepository<Publisher>().GetAllAsync();

            return publishers.Select(l => new PublisherViewModel
            {
                Id = l.Id,
                Name = l.Name
            });
        }
    }
}
