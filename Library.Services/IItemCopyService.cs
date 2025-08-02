using Library.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IItemCopyService
    {
        IEnumerable<ItemCopyViewModel> GetCopiesByLibraryItemId(int itemId);
        void AddCopy(ItemCopyViewModel model);
        void DeleteCopy(int id);
        int CountAvailableCopies(int itemId);

        Task<IEnumerable<SelectListItem>> GetAvailableItemCopiesAsSelectListAsync();
    }
}
