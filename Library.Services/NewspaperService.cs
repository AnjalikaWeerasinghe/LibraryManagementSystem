using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

        public string InsertNewspaper(NewspaperViewModel newspaper)
        {
            var existing = _unitOfWork.GenericRepository<Newspaper>()
                .GetAll()
                .FirstOrDefault(c => c.Title == newspaper.Title && c.ISSN == newspaper.ISSN);

            if (existing != null)
                return "Newspaper already exists.";

            var model = new Newspaper
            {
                Title = newspaper.Title,
                ISSN = newspaper.ISSN,
                Description = newspaper.Description,
                PublisherId = newspaper.PublisherId,
                LanguageId = newspaper.LanguageId,
                CategoryId = newspaper.CategoryId,
                ItemCode = newspaper.ItemCode,
                IssuedDate = newspaper.IssuedDate,
                IssueNumber = newspaper.IssueNumber
            };

            _unitOfWork.GenericRepository<Newspaper>().Add(model);
            _unitOfWork.Save();

            return "Newspaper added successfully.";
        }

        public void UpdateNewspaper(NewspaperViewModel newspaper)
        {
            var ModelById = _unitOfWork.GenericRepository<Newspaper>().GetById(newspaper.Id);
            if (newspaper == null)
                throw new KeyNotFoundException($"Newspaper with Id {newspaper.Id} not found.");

            ModelById.Title = newspaper.Title;
            ModelById.Description = newspaper.Description;
            ModelById.IssuedDate = newspaper.IssuedDate;
            ModelById.IssueNumber = newspaper.IssueNumber;
            ModelById.LanguageId = newspaper.LanguageId;
            ModelById.PublisherId = newspaper.PublisherId;
            ModelById.CategoryId = newspaper.CategoryId;
            ModelById.ISSN = newspaper.ISSN;

            _unitOfWork.GenericRepository<Newspaper>().Update(ModelById);
            _unitOfWork.Save();
        }

        public PagedResult<NewspaperViewModel> GetNewspaperByName(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Newspaper>()
                .GetAll(includeProperties: "Language,Category,Publisher")
                .Where(p => p.Title.Contains(name))
                .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new NewspaperViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ItemCode = p.ItemCode,
                ISSN = p.ISSN,
                IssuedDate = p.IssuedDate,
                IssueNumber = p.IssueNumber,
                LanguageId = p.LanguageId,
                CategoryId = p.CategoryId,
                PublisherId = p.PublisherId,
                Description = p.Description,

            }).ToList();

            return new PagedResult<NewspaperViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public string GenerateNextNewspaperCode()
        {
            var lastnewspaper = _unitOfWork.GenericRepository<Newspaper>()
            .GetAll()
            .OrderByDescending(e => e.Id)
            .FirstOrDefault();

            int lastNumber = 0;

            if (lastnewspaper != null && Regex.IsMatch(lastnewspaper.ItemCode ?? "", @"^ITD-NW-(\d{5})$"))
            {
                var match = Regex.Match(lastnewspaper.ItemCode, @"^ITD-NW-(\d{5})$");
                lastNumber = int.Parse(match.Groups[1].Value);
            }

            return $"ITD-NW-{(lastNumber + 1).ToString("D5")}";
        }
    }
}
