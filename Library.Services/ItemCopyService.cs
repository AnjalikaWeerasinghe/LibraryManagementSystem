using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ItemCopyService : IItemCopyService
    {
        private IUnitOfWork _unitOfWork;

        public ItemCopyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ItemCopyViewModel> GetCopiesByLibraryItemId(int itemId)
        {
            var copies = _unitOfWork.GenericRepository<ItemCopy>()
                .GetAll()
                .Where(c => c.LibraryItemId == itemId)
                .Select(c => new ItemCopyViewModel
                {
                    Id = c.Id,
                    LibraryItemId = c.LibraryItemId,
                    ShelfLocation = c.ShelfLocation,
                    Available = c.Available
                })
                .ToList();

            return copies;
        }

        public void AddCopy(ItemCopyViewModel model)
        {
            var entity = new ItemCopy
            {
                LibraryItemId = model.LibraryItemId,
                ShelfLocation = model.ShelfLocation,
                Available = model.Available
            };

            _unitOfWork.GenericRepository<ItemCopy>().Add(entity);
            _unitOfWork.Save();
        }

        public void DeleteCopy(int id)
        {
            var entity = _unitOfWork.GenericRepository<ItemCopy>().GetById(id);
            if (entity != null)
            {
                _unitOfWork.GenericRepository<ItemCopy>().Delete(entity);
                _unitOfWork.Save();
            }
        }

        public int CountAvailableCopies(int itemId)
        {
            return _unitOfWork.GenericRepository<ItemCopy>()
                .GetAll()
                .Count(c => c.LibraryItemId == itemId && c.Available);
        }

        public async Task<IEnumerable<SelectListItem>> GetAvailableItemCopiesAsSelectListAsync()
        {
            var copies = _unitOfWork.GenericRepository<ItemCopy>()
                .GetAll(i => i.Available == true, includeProperties: "LibraryItem");

            return copies.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.LibraryItem.Title} - (Copy ID: {i.Id})"
            }).ToList();
        }

    }
}
