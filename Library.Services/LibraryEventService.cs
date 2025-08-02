using Library.Models;
using Library.Repositories.Interfaces;
using Library.Utilities;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Services
{
    public class LibraryEventService : ILibraryEventService
    {
        private IUnitOfWork _unitOfWork;

        public LibraryEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteLibraryEvent(int id)
        {
            var model = _unitOfWork.GenericRepository<LibraryEvent>().GetById(id);
            _unitOfWork.GenericRepository<LibraryEvent>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<LibraryEventViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new LibraryEventViewModel();
            int totalCount;
            List<LibraryEventViewModel> vmList = new List<LibraryEventViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<LibraryEvent>().GetAll().
                    Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<LibraryEvent>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<LibraryEventViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;

        }

        private List<LibraryEventViewModel> ConvertModelToViewModelList(List<LibraryEvent> modelList)
        {
            return modelList.Select(x => new LibraryEventViewModel(x)).ToList();
        }

        public LibraryEventViewModel GetLibraryEventById(int LibraryId)
        {
            var model = _unitOfWork.GenericRepository<LibraryEvent>().GetById(LibraryId);
            var vm = new LibraryEventViewModel(model);
            return vm;
        }

        public string InsertLibraryEvent(LibraryEventViewModel libraryEvent)
        {
            var existing = _unitOfWork.GenericRepository<LibraryEvent>()
                    .GetAll()
                    .FirstOrDefault(c => c.Title == libraryEvent.Title && c.StartDate == libraryEvent.StartDate);

            if (existing != null)
                return "Event already exists.";

            var model = new LibraryEvent
            {
                Title = libraryEvent.Title,
                Description = libraryEvent.Description,
                ImageUrl = libraryEvent.ImageUrl,
                StartDate = libraryEvent.StartDate,
                EndDate = libraryEvent.EndDate,
                Location = libraryEvent.Location,
                EventCode = libraryEvent.EventCode,
                EventStatus = libraryEvent.EventStatus
            };

            _unitOfWork.GenericRepository<LibraryEvent>().Add(model);
            _unitOfWork.Save();

            return "Event created successfully.";
        }

        public void UpdateLibraryEvent(LibraryEventViewModel libraryEvent)
        {
            var ModelById = _unitOfWork.GenericRepository<LibraryEvent>().GetById(libraryEvent.Id);
            if (libraryEvent == null)
                throw new KeyNotFoundException($"Library Event with Id {libraryEvent.Id} not found.");

            ModelById.Title = libraryEvent.Title;
            ModelById.Description = libraryEvent.Description;
            ModelById.ImageUrl = libraryEvent.ImageUrl;
            ModelById.StartDate = libraryEvent.StartDate;
            ModelById.EndDate = libraryEvent.EndDate;
            ModelById.Location = libraryEvent.Location;

            _unitOfWork.GenericRepository<LibraryEvent>().Update(ModelById);
            _unitOfWork.Save();
        }

        public PagedResult<LibraryEventViewModel> GetEventByTitle(string name, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.GenericRepository<LibraryEvent>()
                .GetAll()
                .Where(p => p.Title.Contains(name))
                .AsQueryable();

            int totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = data.Select(p => new LibraryEventViewModel
            {
                Id = p.Id,
                EventCode = p.EventCode,
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Location = p.Location,
                EventStatus = p.EventStatus

            }).ToList();

            return new PagedResult<LibraryEventViewModel>
            {
                Data = viewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public string GenerateNextEventCode()
        {
            var lastEvent = _unitOfWork.GenericRepository<LibraryEvent>()
            .GetAll()
            .OrderByDescending(e => e.Id)
            .FirstOrDefault();

            int lastNumber = 0;

            if (lastEvent != null && Regex.IsMatch(lastEvent.EventCode ?? "", @"^EID-(\d{4})$"))
            {
                var match = Regex.Match(lastEvent.EventCode, @"^EID-(\d{4})$");
                lastNumber = int.Parse(match.Groups[1].Value);
            }

            return $"EID-{(lastNumber + 1).ToString("D4")}";
        }

        public void ToggleEventStatusAsync(int eventId)
        {
            var model = _unitOfWork.GenericRepository<LibraryEvent>().GetById(eventId);

            if (model == null)
            {
                throw new Exception("Event not found");
            }
                

            model.EventStatus = model.EventStatus == EventStatus.Ongoing
                ? EventStatus.Cancel
                : EventStatus.Ongoing;

            _unitOfWork.GenericRepository<LibraryEvent>().Update(model);
           
        }


    }

}
