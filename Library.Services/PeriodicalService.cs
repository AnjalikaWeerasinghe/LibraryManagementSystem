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

        public void InsertPeriodical(PeriodicalViewModel periodical)
        {
            if (PeriodicalExists(periodical.Title, periodical.ItemCode, periodical.ISSN))
            {
                throw new Exception("Periodical already exists.");
            }

            var model = new PeriodicalViewModel().ConvertToViewModelToModel(periodical);
            _unitOfWork.GenericRepository<Periodical>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdatePeriodical(PeriodicalViewModel periodical)
        {
            var model = new PeriodicalViewModel().ConvertToViewModelToModel(periodical);
            var ModelById = _unitOfWork.GenericRepository<Periodical>().GetById(model.Id);

            ModelById.ItemCode = periodical.ItemCode;
            ModelById.Title = periodical.Title;
            ModelById.Description = periodical.Description;
            ModelById.PublishedYear = periodical.PublishedYear;
            ModelById.ShelfLocation = periodical.ShelfLocation;
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
            var lastJournal = _unitOfWork.GenericRepository<Periodical>()
            .GetAll()
            .OrderByDescending(e => e.Id)
            .FirstOrDefault();

            int lastNumber = 0;

            if (lastJournal != null && Regex.IsMatch(lastJournal.ItemCode ?? "", @"^ITD-(\d{4})$"))
            {
                var match = Regex.Match(lastJournal.ItemCode, @"^ITD-(\d{4})$");
                lastNumber = int.Parse(match.Groups[1].Value);
            }

            return $"ITD-{(lastNumber + 1).ToString("D4")}";
        }
    }
}
