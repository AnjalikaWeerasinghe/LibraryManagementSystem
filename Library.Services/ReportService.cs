using Library.Models;
using Library.Repositories.Interfaces;
using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ItemCategoryCountViewModel>> GetCountsAsync()
        {
            var flat = await _unitOfWork.GenericRepository<LibraryItem>()
                .GetAll()  // Add GetAll() or IQueryable source if needed
                .Select(i => new
                {
                    i.ItemType,
                    CategoryName = i.Category.Name // rename to avoid conflict with 'Name'
                })
                .ToListAsync();  // SQL ends here

            var data = flat
                .GroupBy(x => new { x.ItemType, x.CategoryName })
                .Select(g => new ItemCategoryCountViewModel
                {
                    ItemType = g.Key.ItemType.ToString(),
                    Category = g.Key.CategoryName,
                    Count = g.Count()
                })
                .OrderBy(x => x.ItemType)
                .ThenBy(x => x.Category)
                .ToList();

            return data;
        }
    } 
}

