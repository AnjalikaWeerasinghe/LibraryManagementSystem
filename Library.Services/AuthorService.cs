using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteAuthor(int id)
        {
            var model = _unitOfWork.GenericRepository<Author>().GetById(id);
            _unitOfWork.GenericRepository<Author>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<AuthorViewModel> GetAll(int pageNumber, int pageSize)
        { 
            var vm = new AuthorViewModel();
            int totalCount;
            List<AuthorViewModel> vmList = new List<AuthorViewModel>();

            try
            {
                int excludeRecords = (pageSize * pageNumber) - pageSize;

                // Include "Country" navigation property
                var modelList = _unitOfWork
                    .GenericRepository<Author>()
                    .GetAll(includeProperties: "Country")
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .ToList();

                totalCount = _unitOfWork.GenericRepository<Author>().GetAll().ToList().Count();

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            return new PagedResult<AuthorViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private List<AuthorViewModel> ConvertModelToViewModelList(List<Author> modelList)
        {
            return modelList.Select(x => new AuthorViewModel(x)).ToList();

            //return authors.Select(a => new AuthorViewModel
            //{
            //    Id = a.Id,
            //    Name = a.Name,
            //    CountryId = a.CountryId,
            //    CountryName = a.Country?.Name,
            //    Biography = a.Biography
            //}).ToList();
        }

        public AuthorViewModel GetAuthorById(int AuthorId)
        {
            var model = _unitOfWork
                .GenericRepository<Author>()
                .GetById(AuthorId);

                //.GetAll(a => a.Id == AuthorId, includeProperties: "Country")
                //.FirstOrDefault();

            if (model == null) return null;

            return new AuthorViewModel(model);
        }

        public PagedResult<AuthorViewModel> GetAuthorByName(string name, int pageNumber, int pageSize)
        {
            var allAuthors = _unitOfWork.GenericRepository<Author>()
                .GetAll(includeProperties: "Country") // Include the Country navigation property
                .Where(p => p.Name.Contains(name));

            int totalCount = allAuthors.Count();

            var data = allAuthors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new AuthorViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CountryId = p.CountryId,
                Biography = p.Biography
            }).ToList();

            return new PagedResult<AuthorViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public string InsertAuthor(AuthorViewModel author)
        {
            var existing = _unitOfWork.GenericRepository<Author>()
                    .GetAll()
                    .FirstOrDefault(c => c.Name == author.Name);

            if (existing != null)
                return "Author already exists.";

            var model = new Author
            {
                Name = author.Name,
                CountryId = author.CountryId,
                Biography = author.Biography
            };

            _unitOfWork.GenericRepository<Author>().Add(model);
            _unitOfWork.Save();

            return "Author added successfully.";

        }

        public void UpdateAuthor(AuthorViewModel author)
        {
            var model = new AuthorViewModel().ConvertViewModel(author);
            var ModelById = _unitOfWork.GenericRepository<Author>().GetById(author.Id);

            ModelById.Name = author.Name;
            ModelById.CountryId = author.CountryId;
            ModelById.Biography = author.Biography;

            _unitOfWork.GenericRepository<Author>().Update(ModelById);
            _unitOfWork.Save();
        }

        public IEnumerable<CountryViewModel> GetAllCountries()
        {
            return _unitOfWork.GenericRepository<Country>()
                .GetAll()
                .Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                });
        }
    }
}
