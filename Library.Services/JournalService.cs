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
            int totalCount;
            var vm = new JournalViewModel();
            List<JournalViewModel> vmList = new List<JournalViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Journal>()
                    .GetAll(includeProperties: "Language,Category,Publisher,FieldOfStudy")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Journal>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);

                var result = new PagedResult<JournalViewModel>
                {
                    Data = vmList,
                    TotalItems = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
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

        public string InsertJournal(JournalViewModel journal)
        {
            var existing = _unitOfWork.GenericRepository<Journal>()
                .GetAll()
                .FirstOrDefault(c => c.Title == journal.Title && c.ISSN == journal.ISSN);

            if (existing != null)
                return "Journal already exists.";

            var model = new Journal
            {
                Title = journal.Title,
                Description = journal.Description,
                PublishedYear = journal.PublishedYear,
                Volume = journal.Volume,
                Issue = journal.Issue,
                LanguageId = journal.LanguageId,
                PublisherId = journal.PublisherId,
                CategoryId = journal.CategoryId,
                ISSN = journal.ISSN,
                FieldOfStudyId = journal.FieldOfStudyId,
                ItemCode = journal.ItemCode
            };
            
            _unitOfWork.GenericRepository<Journal>().Add(model);
            _unitOfWork.Save();

            return "Journal added successfully.";
        }

        public void UpdateJournal(JournalViewModel journal)
        {
           
            var ModelById = _unitOfWork.GenericRepository<Journal>().GetById(journal.Id);
            if (journal == null)
                throw new KeyNotFoundException($"Journal with Id {journal.Id} not found.");

            ModelById.Title = journal.Title;
            ModelById.Description = journal.Description;
            ModelById.PublishedYear = journal.PublishedYear;
            ModelById.Volume = journal.Volume;
            ModelById.Issue = journal.Issue;
            ModelById.LanguageId = journal.LanguageId;
            ModelById.PublisherId = journal.PublisherId;
            ModelById.CategoryId = journal.CategoryId;
            ModelById.ISSN = journal.ISSN;
            ModelById.FieldOfStudyId = journal.FieldOfStudyId;

            _unitOfWork.GenericRepository<Journal>().Update(ModelById);
            _unitOfWork.Save();
        }

        public PagedResult<JournalViewModel> GetJournalByName(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<Journal>()
                .GetAll(includeProperties: "Language,Category,Publisher,FieldOfStudy")
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
                LanguageId = p.LanguageId,
                CategoryId = p.CategoryId,
                PublisherId = p.PublisherId,
                Description = p.Description,
                FieldOfStudyId = p.FieldOfStudyId,

            }).ToList();

            return new PagedResult<JournalViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public string GenerateNextJournalCode()
        {
            var lastJournal = _unitOfWork.GenericRepository<Journal>()
            .GetAll()
            .OrderByDescending(e => e.Id)
            .FirstOrDefault();

            int lastNumber = 0;

            if (lastJournal != null && Regex.IsMatch(lastJournal.ItemCode ?? "", @"^ITD-JR-(\d{5})$"))
            {
                var match = Regex.Match(lastJournal.ItemCode, @"^ITD-JR-(\d{5})$");
                lastNumber = int.Parse(match.Groups[1].Value);
            }

            return $"ITD-JR-{(lastNumber + 1).ToString("D5")}";
        }
    }
}
