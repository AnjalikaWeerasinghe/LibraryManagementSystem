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
    public class JournalService : IJournalService
    {
        private IUnitOfWork _unitOfWork;

        public JournalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteJournal(int id)
        {
            var model = _unitOfWork.GenericRepository<Journal>().GetById(id);
            _unitOfWork.GenericRepository<Journal>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<JournalViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new JournalViewModel();
            int totalCount;
            List<JournalViewModel> vmList = new List<JournalViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Journal>()
                    .GetAll(includeProperties: "Language,Category,Publisher")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Journal>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<JournalViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<JournalViewModel> ConvertModelToViewModelList(List<Journal> modelList)
        {
            return modelList.Select(x => new JournalViewModel(x)).ToList();
        }

        public JournalViewModel GetJournalById(int journalId)
        {
            var model = _unitOfWork.GenericRepository<Journal>().GetById(journalId);
            var vm = new JournalViewModel(model);
            return vm;
        }

        public void InsertJournal(JournalViewModel journal)
        {
            var model = new JournalViewModel().ConvertToViewModelToModel(journal);
            _unitOfWork.GenericRepository<Journal>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateJournal(JournalViewModel journal)
        {
            var model = new JournalViewModel().ConvertToViewModelToModel(journal);
            var ModelById = _unitOfWork.GenericRepository<Journal>().GetById(model.Id);

            ModelById.ItemCode = journal.ItemCode;
            ModelById.Title = journal.Title;
            ModelById.Description = journal.Description;
            ModelById.PublishedYear = journal.PublishedYear;
            ModelById.ShelfLocation = journal.ShelfLocation;
            ModelById.Volume = journal.Volume;
            ModelById.Issue = journal.Volume;
            ModelById.LanguageId = journal.LanguageId;
            ModelById.PublisherId = journal.PublisherId;
            ModelById.CategoryId = journal.CategoryId;
            ModelById.ISSN = journal.ISSN;
            ModelById.Field = journal.Field;

            _unitOfWork.GenericRepository<Journal>().Update(ModelById);
            _unitOfWork.Save();
        }

        public PagedResult<JournalViewModel> GetJournalByName(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Journal>()
                .GetAll()
                .Where(p => p.Title.Contains(name))
                .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new JournalViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ItemCode = p.ItemCode,
                ISSN = p.ISSN,
                Volume = p.Volume,
                Issue = p.Issue,
                ShelfLocation = p.ShelfLocation,
                LanguageId = p.LanguageId,
                CategoryId = p.CategoryId,
                PublisherId = p.PublisherId,
                Description = p.Description,

            }).ToList();

            return new PagedResult<JournalViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        //public string GenerateNextJournalCode()
        //{
        //    var lastJournal = _unitOfWork.GenericRepository<Journal>()
        //    .GetAll()
        //    .OrderByDescending(e => e.Id)
        //    .FirstOrDefault();

        //    int lastNumber = 0;

        //    if (lastJournal != null && Regex.IsMatch(lastJournal.ItemCode ?? "", @"^ITD(\d{4})$"))
        //    {
        //        var match = Regex.Match(lastJournal.ItemCode, @"^ITD-(\d{4})$");
        //        lastNumber = int.Parse(match.Groups[1].Value);
        //    }

        //    return $"ITD-{(lastNumber + 1).ToString("D4")}";
        //}
    }
}
