using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Services
{
    public class PeriodicalService : IPeriodicalService
    {
        private IUnitOfWork _unitOfWork;

        public PeriodicalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool PeriodicalExists(string itemcode, string issn, string title)
        {
            return _unitOfWork.GenericRepository<Periodical>().GetAll()
                .Any(p => p.Title.ToLower() == title.ToLower() &&
                          p.ItemCode == itemcode &&
                          p.ISSN == issn);
        }

        public void DeletePeriodical(int id)
        {
            var model = _unitOfWork.GenericRepository<Periodical>().GetById(id);
            _unitOfWork.GenericRepository<Periodical>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<PeriodicalViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new PeriodicalViewModel();
            int totalCount;
            List<PeriodicalViewModel> vmList = new List<PeriodicalViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Periodical>()
                    .GetAll(includeProperties: "Language,Category,Publisher")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Periodical>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<PeriodicalViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<PeriodicalViewModel> ConvertModelToViewModelList(List<Periodical> modelList)
        {
            return modelList.Select(x => new PeriodicalViewModel(x)).ToList();
        }

        public PeriodicalViewModel GetPeriodicalById(int periodicalId)
        {
            var model = _unitOfWork.GenericRepository<Periodical>().GetById(periodicalId);
            var vm = new PeriodicalViewModel(model);
            return vm;
        }

        public string InsertPeriodical(PeriodicalViewModel periodical)
        {
            var existing = _unitOfWork.GenericRepository<Periodical>()
                .GetAll()
                .FirstOrDefault(c => c.Title == periodical.Title && c.ISSN == periodical.ISSN);

            if (existing != null)
                return "Periodical already exists.";

            var model = new Periodical
            {
                Title = periodical.Title,
                Description = periodical.Description,
                PublishedYear = periodical.PublishedYear,
                LanguageId = periodical.LanguageId,
                PublisherId = periodical.PublisherId,
                CategoryId = periodical.CategoryId,
                ISSN = periodical.ISSN,
                ItemCode = periodical.ItemCode,
                Frequency = periodical.Frequency,
                Theme = periodical.Theme
            };
            
            _unitOfWork.GenericRepository<Periodical>().Add(model);
            _unitOfWork.Save();

            return "Periodical added successfully.";
        }

        public void UpdatePeriodical(PeriodicalViewModel periodical)
        {
            var ModelById = _unitOfWork.GenericRepository<Periodical>().GetById(periodical.Id);
            if (periodical == null)
                throw new KeyNotFoundException($"Periodical with Id {periodical.Id} not found.");

            ModelById.Title = periodical.Title;
            ModelById.Description = periodical.Description;
            ModelById.PublishedYear = periodical.PublishedYear;
            ModelById.LanguageId = periodical.LanguageId;
            ModelById.PublisherId = periodical.PublisherId;
            ModelById.CategoryId = periodical.CategoryId;
            ModelById.ISSN = periodical.ISSN;
            ModelById.Frequency = periodical.Frequency;
            ModelById.Theme = periodical.Theme;

            _unitOfWork.GenericRepository<Periodical>().Update(ModelById);
            _unitOfWork.Save();
        }

        public string GenerateNextPeriodicalCode()
        {
            var lastPeriodical = _unitOfWork.GenericRepository<Periodical>()
            .GetAll()
            .OrderByDescending(e => e.Id)
            .FirstOrDefault();

            int lastNumber = 0;

            if (lastPeriodical != null && Regex.IsMatch(lastPeriodical.ItemCode ?? "", @"^ITD-PR-(\d{5})$"))
            {
                var match = Regex.Match(lastPeriodical.ItemCode, @"^ITD-PR-(\d{5})$");
                lastNumber = int.Parse(match.Groups[1].Value);
            }

            return $"ITD-PR-{(lastNumber + 1).ToString("D5")}";
        }

        public PagedResult<PeriodicalViewModel> GetPeriodicalByName(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Periodical>()
                .GetAll(includeProperties: "Language,Category,Publisher")
                .Where(p => p.Title.Contains(name))
                .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new PeriodicalViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ItemCode = p.ItemCode,
                ISSN = p.ISSN,
                PublishedYear = p.PublishedYear,
                Frequency = p.Frequency,
                Theme = p.Theme,
                LanguageId = p.LanguageId,
                CategoryId = p.CategoryId,
                PublisherId = p.PublisherId,
                Description = p.Description,

            }).ToList();

            return new PagedResult<PeriodicalViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
